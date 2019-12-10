using MediatR;

namespace MessageBoard.Application.Messages.Commands
{
    public class CreateMessageCommand : IRequest<MessageDto>
    {
        public CreateMessageCommand(string message, int clientId)
        {
            Message = message;
            ClientId = clientId;
        }

        public string Message { get; }

        public int ClientId { get; }
    }
}
