using MessageBoard.Application.Messages.Commands;
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
        public async Task Should_create_new_message_when_input_data_is_valid()
        {
            // Arrange
            _mockedRepository.Setup(x => x.UnitOfWork).Returns(_mockedUnitOfWork.Object);
            _mockedUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new CreateMessageCommand("New message", 1);
            var handler = new CreateMessageCommandHandler(_mockedRepository.Object);

            // Act
            var createdMessage = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(createdMessage);
            Assert.Equal(command.Message, createdMessage.Message);
            Assert.Equal(command.ClientId, createdMessage.ClientId);

            _mockedRepository.Verify(x => x.Add(It.IsAny<Message>()), Times.Once);
            _mockedUnitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
