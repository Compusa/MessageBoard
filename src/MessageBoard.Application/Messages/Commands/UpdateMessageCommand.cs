using MediatR;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class UpdateMessageCommand : IRequest<MessageDto>
    {
        public UpdateMessageCommand(int messageId, string message, int clientId)
        {
            MessageId = messageId;
            Message = message;
            ClientId = clientId;
        }

        public int MessageId { get; }

        public string Message { get; }

        public int ClientId { get; }
    }
}
