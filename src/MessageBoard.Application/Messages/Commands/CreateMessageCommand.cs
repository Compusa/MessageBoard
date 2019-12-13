using MediatR;
using MessageBoard.Application.SeedWork.Results;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Application.Messages.Commands
{
    public class CreateMessageCommand : IRequest<Result<MessageDto>>
    {
        public CreateMessageCommand(string message, string clientId)
        {
            Message = message?.Trim();
            ClientId = clientId;
        }

        public string Message { get; }

        public string ClientId { get; }
    }
}
