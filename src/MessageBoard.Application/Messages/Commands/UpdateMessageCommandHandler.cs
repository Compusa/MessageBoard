using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, MessageDto>
    {
        private readonly IMessageRepository _messageRepository;

        public UpdateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageDto> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetAsync(request.MessageId);

            if (message == null)
            {
                return null; // NOT FOUND
            }

            if (message.ClientId != request.ClientId)
            {
                return null;
            }

            message.UpdateContent(request.Message);

            await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return MessageDto.Map(message);
        }
    }
}
