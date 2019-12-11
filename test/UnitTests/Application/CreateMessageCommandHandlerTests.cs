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
    public class CreateMessageCommandHandlerTests
    {
        [Fact]
        public async Task Should_create_new_message_when_input_data_is_valid()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var repositoryMock = new Mock<IMessageRepository>();
            repositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);

            var command = new CreateMessageCommand(string.Empty, 0);
            var handler = new CreateMessageCommandHandler(repositoryMock.Object);

            // Act
            var messageDto = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(messageDto);

            repositoryMock.Verify(x => x.Add(It.IsAny<Message>()), Times.Once);
            repositoryMock.Verify(x => x.UnitOfWork, Times.Once);

            unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
