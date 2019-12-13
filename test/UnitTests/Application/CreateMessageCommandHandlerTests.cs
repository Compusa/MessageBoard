using MessageBoard.Application.Messages.Commands;
using MessageBoard.Application.SeedWork.Results.StatusCodes;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application
{
    public class CreateMessageCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IBoardMessageRepository> _mockedRepository;

        public CreateMessageCommandHandlerTests()
        {
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedRepository = new Mock<IBoardMessageRepository>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public async Task Should_fail_with_status_bad_request_when_message_is_null_or_empty(string message)
        {
            // Arrange
            var command = new CreateMessageCommand(message, Guid.NewGuid().ToString());
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public async Task Should_fail_with_status_bad_request_when_client_id_is_null_or_empty(string clientId)
        {
            // Arrange
            var command = new CreateMessageCommand("The message", clientId);
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

            var command = new CreateMessageCommand("New message", Guid.NewGuid().ToString());
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Succeeded);
            Assert.IsType<Created>(result.StatusCode);
            Assert.Equal(command.Message, result.Value.Message);
            Assert.Equal(command.ClientId, result.Value.ClientId);

            _mockedRepository.Verify(x => x.Add(It.IsAny<BoardMessage>()), Times.Once);
            _mockedUnitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
