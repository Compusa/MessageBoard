using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Application.SeedWork.Results;
using MessageBoard.Application.SeedWork.Results.StatusCodes;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Result>
    {
        private readonly IBoardMessageRepository _messageRepository;

        public UpdateMessageCommandHandler(IBoardMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Result> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
            {
                return Result.Fail<BadRequest, MessageDto>("ClientId is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return Result.Fail<BadRequest>("Message is required.");
            }

            var message = await _messageRepository.GetAsync(request.MessageId);

            if (message == null)
            {
                return Result.Fail<NotFound>();
            }

            if (!string.Equals(message.ClientId, request.ClientId, StringComparison.OrdinalIgnoreCase))
            {
                return Result.Fail<BadRequest>("The message can only be updated by the client that created it.");
            }

            message.UpdateContent(request.Message);
            _messageRepository.Update(message);

            await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok<Updated>();
        }
    }
}
