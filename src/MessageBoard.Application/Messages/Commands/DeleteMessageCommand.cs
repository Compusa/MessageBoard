using MediatR;
using MessageBoard.Application.SeedWork.Results;

namespace MessageBoard.Application.Messages.Commands
{
    public class DeleteMessageCommand : IRequest<Result>
    {
        public DeleteMessageCommand(int messageId, string clientId)
        {
            MessageId = messageId;
            ClientId = clientId;
        }

        public int MessageId { get; }

        public string ClientId { get; }
    }
}
