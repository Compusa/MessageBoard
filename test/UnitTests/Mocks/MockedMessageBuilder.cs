using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Moq;
using System;

namespace UnitTests.Mocks
{
    public sealed class MockedMessageBuilder
    {
        private readonly int _messageId;

        private string _content;

        private string _clientId;

        private MockedMessageBuilder(int messageId)
        {
            _messageId = messageId;
        }

        public static MockedMessageBuilder SetId(int id)
        {
            return new MockedMessageBuilder(id);
        }

        public MockedMessageBuilder SetMessage(string content)
        {
            _content = content;

            return this;
        }

        public MockedMessageBuilder SetClientId(string clientId)
        {
            _clientId = clientId;

            return this;
        }

        public Mock<BoardMessage> Build()
        {
            var messageMock = new Mock<BoardMessage>(_content, _clientId, DateTimeOffset.UtcNow);
            messageMock.Setup(x => x.Id).Returns(_messageId);

            return messageMock;
        }
    }
}
