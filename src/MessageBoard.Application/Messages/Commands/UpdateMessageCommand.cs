﻿using MediatR;
using MessageBoard.Application.SeedWork.Results;

namespace MessageBoard.Application.Messages.Commands
{
    public class UpdateMessageCommand : IRequest<Result>
    {
        public UpdateMessageCommand(int messageId, string message, string clientId)
        {
            MessageId = messageId;
            Message = message;
            ClientId = clientId;
        }

        public int MessageId { get; }

        public string Message { get; }

        public string ClientId { get; }
    }
}
