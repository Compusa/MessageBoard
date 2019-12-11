using MessageBoard.Application.Messages.Commands;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Application
{
    public class DeleteMessageHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IMessageRepository> _mockedRepository;

        public DeleteMessageHandlerTests()
        {
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedRepository = new Mock<IMessageRepository>();
        }

        [Fact]
        public async Task Should_return_false_when_deleting_message_that_does_not_exist()
        {
            // Arrange
            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Message)null);

            var command = new DeleteMessageCommand(1, 2);
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var isDeleted = await handler.Handle(command, default);

            // Assert
            Assert.False(isDeleted);

            _mockedRepository.Verify(x => x.GetAsync(command.MessageId), Times.Once);
        }

        [Fact]
        public async Task Should_return_false_when_attempting_to_delete_message_created_by_another_client()
        {
            // Arrange
            var message = MockedMessageBuilder
                .SetId(1)
                .SetContent("The message")
                .SetClientId(1)
                .Build()
                .Object;

            var command = new DeleteMessageCommand(message.Id, 2);
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var isDeleted = await handler.Handle(command, default);

            // Assert
            Assert.False(isDeleted);

            _mockedRepository.Verify(x => x.GetAsync(command.MessageId), Times.Once);
        }

        [Fact]
        public async Task Should_return_true_when_delete_is_valid()
        {
            // Arrange
            var message = MockedMessageBuilder
                .SetId(1)
                .SetContent("The message")
                .SetClientId(1)
                .Build()
                .Object;

            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(message);
            _mockedRepository.Setup(x => x.UnitOfWork).Returns(_mockedUnitOfWork.Object);
            _mockedUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new DeleteMessageCommand(message.Id, message.ClientId);
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var isDeleted = await handler.Handle(command, default);

            // Assert
            Assert.True(isDeleted);

            _mockedRepository.Verify(x => x.GetAsync(command.MessageId), Times.Once);
            _mockedRepository.Verify(x => x.Remove(message), Times.Once);
            _mockedUnitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
