using MessageBoard.Application.Messages.Commands;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Application.Mocks;
using Xunit;

namespace UnitTests.Application
{
    public class UpdateMessageHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IMessageRepository> _mockedRepository;

        public UpdateMessageHandlerTests()
        {
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedRepository = new Mock<IMessageRepository>();
        }

        [Fact]
        public async Task Should_return_null_when_updating_message_that_does_not_exist()
        {
            // Arrange
            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Message)null);

            var command = new UpdateMessageCommand(1, "The message", 2);
            var handler = new UpdateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var updatedMessage = await handler.Handle(command, default);

            // Assert
            Assert.Null(updatedMessage);

            _mockedRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_return_null_when_attempting_to_update_message_created_by_another_client()
        {
            // Arrange
            var message = MockedMessageBuilder
                .WithMessageId(1)
                .WithContent("The message")
                .WithClientId(1)
                .Build()
                .Object;

            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(message);

            var command = new UpdateMessageCommand(message.Id, "Attempt to update another client's message", 2);
            var handler = new UpdateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var updatedMessage = await handler.Handle(command, default);

            // Assert
            Assert.Null(updatedMessage);

            _mockedRepository.VerifyAll();
        }

        [Fact]
        public async Task Should_return_updated_message_when_command_values_adheres_to_requirements()
        {
            // Arrange
            var message = MockedMessageBuilder
                .WithMessageId(1)
                .WithContent("The message")
                .WithClientId(1)
                .Build()
                .Object;

            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(message);
            _mockedRepository.Setup(x => x.UnitOfWork).Returns(_mockedUnitOfWork.Object);
            _mockedUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new UpdateMessageCommand(message.Id, "Successful update", message.ClientId);
            var handler = new UpdateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var updatedMessage = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(updatedMessage);
            Assert.Equal(command.MessageId, updatedMessage.Id);
            Assert.Equal(command.Message, updatedMessage.Message);

            _mockedRepository.VerifyAll();
            _mockedUnitOfWork.VerifyAll();
        }
    }
}
