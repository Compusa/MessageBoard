using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Moq;

namespace UnitTests.Application.Mocks
{
    public sealed class MockedMessageBuilder
    {
        private readonly int _messageId;

        private string _content;

        private int _clientId;

        private MockedMessageBuilder(int messageId)
        {
            _messageId = messageId;
        }

        public static MockedMessageBuilder WithMessageId(int id)
        {
            return new MockedMessageBuilder(id);
        }

        public MockedMessageBuilder WithContent(string content)
        {
            _content = content;

            return this;
        }

        public MockedMessageBuilder WithClientId(int clientId)
        {
            _clientId = clientId;

            return this;
        }

        public Mock<Message> Build()
        {
            var messageMock = new Mock<Message>(_content, _clientId);
            messageMock.Setup(x => x.Id).Returns(_messageId);

            return messageMock;
        }
    }
}
