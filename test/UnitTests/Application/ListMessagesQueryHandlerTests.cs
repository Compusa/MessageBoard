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
        [Fact]
        public async Task Should_return_empty_array_when_no_messages_exist()
        {
            // Arrange
            var messages = new List<Message>();

            var contextMock = new Mock<IReadOnlyMessageBoardContext>();
            contextMock.Setup(x => x.Messages).Returns(messages.AsQueryable().BuildMock().Object);

            var query = new ListMessagesQuery();
            var handler = new ListMessagesQueryHandler(contextMock.Object);

            // Act
            var messagesDto = await handler.Handle(query, default);

            // Assert
            Assert.Empty(messagesDto);
            contextMock.Verify(x => x.Messages, Times.Once);
        }

        [Fact]
        public async Task Should_return_non_empty_array_when_message_exists()
        {
            // Arrange
            var messages = new List<Message>
            {
                new Message()
            };

            var contextMock = new Mock<IReadOnlyMessageBoardContext>();
            contextMock.Setup(x => x.Messages).Returns(messages.AsQueryable().BuildMock().Object);

            var query = new ListMessagesQuery();
            var handler = new ListMessagesQueryHandler(contextMock.Object);

            // Act
            var messagesDto = await handler.Handle(query, default);

            // Assert
            Assert.NotEmpty(messagesDto);
            contextMock.Verify(x => x.Messages, Times.Once);
        }

        [Fact]
        public async Task Should_return_expected_number_of_items()
        {
            // Arrange
            var messages = new List<Message>
            {
                new Message(),
                new Message(),
                new Message()
            };

            var contextMock = new Mock<IReadOnlyMessageBoardContext>();
            contextMock.Setup(x => x.Messages).Returns(messages.AsQueryable().BuildMock().Object);

            var query = new ListMessagesQuery();
            var handler = new ListMessagesQueryHandler(contextMock.Object);

            // Act
            var messagesDto = await handler.Handle(query, default);

            // Assert
            Assert.NotEmpty(messagesDto);
            Assert.Equal(messages.Count, messagesDto.Count());
            contextMock.Verify(x => x.Messages, Times.Once);
        }
    }
}
