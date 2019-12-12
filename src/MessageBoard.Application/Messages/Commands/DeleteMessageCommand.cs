using MediatR;
using MessageBoard.Application.SeedWork.Results;

namespace MessageBoard.Application.Messages.Commands
{
    public class DeleteMessageCommand : IRequest<Result>
    {
        public DeleteMessageCommand(int messageId, int clientId)
        {
            MessageId = messageId;
            ClientId = clientId;
        }

        public int MessageId { get; set; }

        public int ClientId { get; set; }
    }
}
