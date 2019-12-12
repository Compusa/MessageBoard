using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Message { get; set; }

        public static MessageDto Create(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                ClientId = message.ClientId,
                Message = message.Content
            };
        }
    }
}
