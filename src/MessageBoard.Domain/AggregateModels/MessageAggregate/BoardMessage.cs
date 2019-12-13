using MessageBoard.Domain.Exceptions;
using MessageBoard.Domain.SeedWork;
using System;

namespace MessageBoard.Domain.AggregateModels.MessageAggregate
{
    public class BoardMessage : Entity, IAggregateRoot
    {
        protected BoardMessage(string message, int clientId, DateTimeOffset createdAt)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new MessageBoardDomainException($"{nameof(message)} is required, null or empty not allowed.");
            }

            if (clientId < 0)
            {
                throw new MessageBoardDomainException($"{nameof(clientId)} is required, null or empty not allowed.");
            }

            Message = message;
            ClientId = clientId;
            CreatedAt = createdAt;
        }

        public int ClientId { get; private set; }

        public string Message { get; private set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public void UpdateContent(string content)
        {
            Message = content;
            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public static BoardMessage Create(string message, int clientId)
        {
            return new BoardMessage(message, clientId, DateTimeOffset.UtcNow);
        }
    }
}
