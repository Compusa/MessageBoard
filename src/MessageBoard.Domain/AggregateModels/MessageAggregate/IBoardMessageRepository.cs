using MessageBoard.Domain.SeedWork;
using System.Threading.Tasks;

namespace MessageBoard.Domain.AggregateModels.MessageAggregate
{
    public interface IBoardMessageRepository : IRepository<BoardMessage>
    {
        BoardMessage Add(BoardMessage message);

        void Update(BoardMessage message);

        void Remove(BoardMessage message);

        Task<BoardMessage> GetAsync(int messageId);
    }
}
