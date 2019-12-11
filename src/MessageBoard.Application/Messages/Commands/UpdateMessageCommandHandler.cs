using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;

        public UpdateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetAsync(request.MessageId);

            if (message == null)
            {
                return false; // NOT FOUND
            }

            if (message.ClientId != request.ClientId)
            {
                return false;
            }

            message.UpdateContent(request.Message);

            var result = await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
