using MessageBoard.Application.Messages.Queries;
using MessageBoard.Domain;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MockQueryable.Moq;

namespace UnitTests.Application
{
    public class ListMessagesQueryHandlerTests
    {
        private readonly Mock<IReadOnlyMessageBoardContext> _mockedReadOnlyContext;

        public ListMessagesQueryHandlerTests()
        {
            _mockedReadOnlyContext = new Mock<IReadOnlyMessageBoardContext>();
        }

        [Fact]
        public async Task Should_return_empty_array_when_no_messages_exist()
        {
            // Arrange
            var messages = new List<Message>();

            _mockedReadOnlyContext.Setup(x => x.Messages).Returns(messages.AsQueryable().BuildMock().Object);

            var query = new ListMessagesQuery();
            var handler = new ListMessagesQueryHandler(_mockedReadOnlyContext.Object);

            // Act
            var messagesDto = await handler.Handle(query, default);

            // Assert
            Assert.Empty(messagesDto);
            _mockedReadOnlyContext.Verify(x => x.Messages, Times.Once);
        }

        [Fact]
        public async Task Should_return_non_empty_array_when_message_exists()
        {
            // Arrange
            var messages = new List<Message>
            {
                new Message("The message", 1)
            };

            _mockedReadOnlyContext.Setup(x => x.Messages).Returns(messages.AsQueryable().BuildMock().Object);

            var query = new ListMessagesQuery();
            var handler = new ListMessagesQueryHandler(_mockedReadOnlyContext.Object);

            // Act
            var messagesDto = await handler.Handle(query, default);

            // Assert
            Assert.NotEmpty(messagesDto);
            _mockedReadOnlyContext.Verify(x => x.Messages, Times.Once);
        }

        [Fact]
        public async Task Should_return_expected_number_of_items()
        {
            // Arrange
            var messages = new List<Message>
            {
                new Message("First message", 1),
                new Message("Second message", 2),
                new Message("Third message", 3)
            };

            _mockedReadOnlyContext.Setup(x => x.Messages).Returns(messages.AsQueryable().BuildMock().Object);

            var query = new ListMessagesQuery();
            var handler = new ListMessagesQueryHandler(_mockedReadOnlyContext.Object);

            // Act
            var messagesDto = await handler.Handle(query, default);

            // Assert
            Assert.NotEmpty(messagesDto);
            Assert.Equal(messages.Count, messagesDto.Count());

            _mockedReadOnlyContext.Verify(x => x.Messages, Times.Once);
        }
    }
}
