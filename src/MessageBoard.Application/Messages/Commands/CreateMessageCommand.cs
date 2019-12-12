using MediatR;
using MessageBoard.Application.SeedWork.Results;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Application.Messages.Commands
{
    public class CreateMessageCommand : IRequest<Result<MessageDto>>
    {
        public CreateMessageCommand()
        {
        }

        public CreateMessageCommand(string message, int clientId)
        {
            Message = message?.Trim();
            ClientId = clientId;
        }

        public string Message { get; set; }

        public int ClientId { get; set; }
    }
}
