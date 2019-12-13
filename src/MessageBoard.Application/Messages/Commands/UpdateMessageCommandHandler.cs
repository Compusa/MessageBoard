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
            var message = await _messageRepository.GetAsync(request.MessageId);

            if (message == null)
            {
                return Result.Fail<NotFound>($"Message with id '{request.MessageId}' not found.");
            }

            if (message.ClientId != request.ClientId)
            {
                return Result.Fail<Forbidden>("The message can only be deleted by the client that created it.");
            }

            message.UpdateContent(request.Message);
            _messageRepository.Update(message);

            await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok<Updated>();
        }
    }
}
