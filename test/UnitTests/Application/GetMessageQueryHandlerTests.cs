using MessageBoard.Application.Messages.Queries;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application
{
    public class GetMessageQueryHandlerTests
    {
        [Fact]
        public async Task Should_return_null_when_message_with_doest_not_exist()
        {
            // Arrange
            const int messageId = 1;
            var handler = new GetMessageQueryHandler();
            var query = new GetMessageQuery(messageId);

            // Act
            var message = await handler.Handle(query, default);

            // Assert
            Assert.Null(message);
        }
    }
}
