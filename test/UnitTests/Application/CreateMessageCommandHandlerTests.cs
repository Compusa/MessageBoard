using MessageBoard.Application.Messages.Commands;
using MessageBoard.Application.SeedWork.Results.StatusCodes;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application
{
    public class CreateMessageCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IMessageRepository> _mockedRepository;

        public CreateMessageCommandHandlerTests()
        {
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedRepository = new Mock<IMessageRepository>();
        }

        [Fact]
        public async Task Should_fail_with_status_bad_request_when_message_is_null()
        {
            // Arrange
            var command = new CreateMessageCommand(null, 1);
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);
        }

        [Fact]
        public async Task Should_fail_with_status_bad_request_when_message_is_empty()
        {
            // Arrange
            var command = new CreateMessageCommand(string.Empty, 1);
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);
        }

        [Fact]
        public async Task Should_fail_with_status_bad_request_when_message_is_white_spaces_only()
        {
            // Arrange
            var command = new CreateMessageCommand("    ", 1);
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);
        }

        [Fact]
        public async Task Should_fail_with_status_bad_request_when_client_id_is_not_specified()
        {
            // Arrange
            var command = new CreateMessageCommand("The message", 0);
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);
        }


        [Fact]
        public async Task Should_succeed_with_status_created_when_create_is_valid()
        {
            // Arrange
            _mockedRepository.Setup(x => x.UnitOfWork).Returns(_mockedUnitOfWork.Object);
            _mockedUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new CreateMessageCommand("New message", 1);
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Succeeded);
            Assert.IsType<Created>(result.StatusCode);
            Assert.Equal(command.Message, result.Value.Message);
            Assert.Equal(command.ClientId, result.Value.ClientId);

            _mockedRepository.Verify(x => x.Add(It.IsAny<Message>()), Times.Once);
            _mockedUnitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
