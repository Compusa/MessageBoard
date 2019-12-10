using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Domain.AggregateModels.MessageAggregate;

namespace MessageBoard.Application.Messages.Commands
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageDto>
    {
        private readonly IMessageRepository _messageRepository;

        public CreateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message(request.Message, request.ClientId);

            _messageRepository.Add(message);

            await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return MessageDto.Map(message);
        }
    }
}
