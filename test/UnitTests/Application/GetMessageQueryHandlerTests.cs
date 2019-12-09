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
            var query = new GetMessageQuery(messageId);

            var context = new Mock<IReadOnlyMessageBoardContext>();
            var handler = new GetMessageQueryHandler(context.Object);

            // Act
            var messageDto = await handler.Handle(query, default);

            // Assert
            Assert.Null(messageDto);
            context.Verify(x => x.FindAsync<Message>(messageId), Times.Once);
        }

        [Fact]
        public async Task Should_return_message_when_specified_id_exists()
        {
            // Arrange
            const int messageId = 1;
            var query = new GetMessageQuery(messageId);

            var message = new Message
            {
                Id = messageId
            };

            var context = new Mock<IReadOnlyMessageBoardContext>();
            context.Setup(x => x.FindAsync<Message>(It.IsAny<int>())).ReturnsAsync(message);
            var handler = new GetMessageQueryHandler(context.Object);

            // Act
            var messageDto = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(messageDto);
            Assert.Equal(messageId, messageDto.Id);
            context.Verify(x => x.FindAsync<Message>(messageId), Times.Once);
        }
    }
}
