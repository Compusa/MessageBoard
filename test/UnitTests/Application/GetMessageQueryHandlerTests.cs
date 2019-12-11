using MessageBoard.Application.Messages.Queries;
using MessageBoard.Domain;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application
{
    public class GetMessageQueryHandlerTests
    {
        [Fact]
        public async Task Should_return_null_when_specified_id_doest_not_exist()
        {
            // Arrange
            const int messageId = 1;

            var contextMock = new Mock<IReadOnlyMessageBoardContext>();

            var query = new GetMessageQuery(messageId);
            var handler = new GetMessageQueryHandler(contextMock.Object);

            // Act
            var messageDto = await handler.Handle(query, default);

            // Assert
            Assert.Null(messageDto);
            contextMock.Verify(x => x.FindAsync<Message>(messageId), Times.Once);
        }

        [Fact]
        public async Task Should_return_message_when_specified_id_exists()
        {
            // Arrange
            const int messageId = 1;

            var contextMock = new Mock<IReadOnlyMessageBoardContext>();
            contextMock
                .Setup(x => x.FindAsync<Message>(messageId))
                .ReturnsAsync(() => new Message(string.Empty, 0));

            var query = new GetMessageQuery(messageId);
            var handler = new GetMessageQueryHandler(contextMock.Object);

            // Act
            var messageDto = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(messageDto);
            contextMock.Verify(x => x.FindAsync<Message>(messageId), Times.Once);
        }
    }
}
