using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBoard.Application.Messages.Queries
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageDto>
    {
        public async Task<MessageDto> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return null;
        }
    }
}
