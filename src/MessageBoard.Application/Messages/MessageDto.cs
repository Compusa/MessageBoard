using MessageBoard.Domain.AggregateModels.MessageAggregate;
using System;

namespace MessageBoard.Application
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Message { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public static MessageDto Create(BoardMessage boardMessage)
        {
            return new MessageDto
            {
                Id = boardMessage.Id,
                ClientId = boardMessage.ClientId,
                Message = boardMessage.Message,
                CreatedAt = boardMessage.CreatedAt,
                ModifiedAt = boardMessage.ModifiedAt
            };
        }
    }
}
