using MediatR;
using System.Collections.Generic;

namespace MessageBoard.Application.Messages.Queries
{
    public class ListMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
    }
}
