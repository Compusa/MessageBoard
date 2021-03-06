﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Application.SeedWork.Results;
using MessageBoard.Application.SeedWork.Results.StatusCodes;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<MessageDto>>
    {
        private readonly IBoardMessageRepository _messageRepository;

        public CreateMessageCommandHandler(IBoardMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // TODO: Use FluentValidation for CreateMessageCommand to get rid of the validation of the params in the handler
        public async Task<Result<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return Result.Fail<BadRequest, MessageDto>("Message is required.");
            }

            if (string.IsNullOrWhiteSpace(request.ClientId))
            {
                return Result.Fail<BadRequest, MessageDto>("ClientId is required.");
            }

            var message = BoardMessage.Create(request.Message, request.ClientId);

            _messageRepository.Add(message);

            await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var messageDto = MessageDto.Create(message);

            return Result.Ok<Created, MessageDto>(messageDto);
        }
    }
}
