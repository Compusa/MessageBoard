using MediatR;
using MessageBoard.Application.SeedWork.Results;

namespace MessageBoard.Application.Messages.Commands
{
    public class UpdateMessageCommand : IRequest<Result>
    {
        public UpdateMessageCommand()
        {
        }

        public UpdateMessageCommand(int messageId, string message, int clientId)
        {
            MessageId = messageId;
            Message = message;
            ClientId = clientId;
        }

        internal int MessageId { get; set; }

        public string Message { get; set; }

        public int ClientId { get; set; }
    }
}
