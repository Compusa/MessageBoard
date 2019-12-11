using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetAsync(request.MessageId);

            if (message == null)
            {
                return false;
            }

            if (message.ClientId != request.ClientId)
            {
            }

            _messageRepository.Remove(message);

            var result = await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
