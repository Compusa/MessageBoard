using MessageBoard.Application.Messages.Queries;
using MessageBoard.Domain;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Moq;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Application
{
    public class GetMessageQueryHandlerTests
    {
        private readonly Mock<IReadOnlyMessageBoardContext> _mockedReadOnlyContext;

        public GetMessageQueryHandlerTests()
        {
            _mockedReadOnlyContext = new Mock<IReadOnlyMessageBoardContext>();
        }

        [Fact]
        public async Task Should_return_null_when_specified_id_doest_not_exist()
        {
            // Arrange
            _mockedReadOnlyContext
                .Setup(x => x.FindAsync<Message>(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var query = new GetMessageQuery(1);
            var handler = new GetMessageQueryHandler(_mockedReadOnlyContext.Object);

            // Act
            var messageDto = await handler.Handle(query, default);

            // Assert
            Assert.Null(messageDto);
            _mockedReadOnlyContext.Verify(x => x.FindAsync<Message>(query.Id), Times.Once);
        }

        [Fact]
        public async Task Should_return_message_when_specified_id_exists()
        {
            // Arrange
            var message = MockedMessageBuilder
                .SetId(1)
                .SetContent("The message")
                .SetClientId(2)
                .Build().Object;

            _mockedReadOnlyContext
                .Setup(x => x.FindAsync<Message>(It.IsAny<int>()))
                .ReturnsAsync(() => message);

            var query = new GetMessageQuery(message.Id);
            var handler = new GetMessageQueryHandler(_mockedReadOnlyContext.Object);

            // Act
            var messageDto = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(messageDto);
            _mockedReadOnlyContext.Verify(x => x.FindAsync<Message>(message.Id), Times.Once);
        }
    }
}
