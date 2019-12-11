using MessageBoard.Domain.SeedWork;

namespace MessageBoard.Domain.AggregateModels.MessageAggregate
{
    public class Message : Entity, IAggregateRoot
    {
        public Message(string content, int clientId)
        {
            Content = content;
            ClientId = clientId;
        }

        public int ClientId { get; }

        public string Content { get; set; }
    }
}
