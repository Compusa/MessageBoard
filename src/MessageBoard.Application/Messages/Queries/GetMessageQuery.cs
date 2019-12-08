using MediatR;

namespace MessageBoard.Application.Messages.Queries
{
    public class GetMessageQuery : IRequest<MessageDto>
    {
        public GetMessageQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
