using MediatR;
using MessageBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBoard.Application.Messages.Queries
{
    public class ListMessagesQueryHandler : IRequestHandler<ListMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IReadOnlyMessageBoardContext _messageBoardContext;

        public ListMessagesQueryHandler(IReadOnlyMessageBoardContext messageBoardContext)
        {
            _messageBoardContext = messageBoardContext;
        }

        public async Task<IEnumerable<MessageDto>> Handle(ListMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _messageBoardContext
                .Messages
                .Select(x => MessageDto.Create(x))
                .ToListAsync(cancellationToken);
        }
    }
}
