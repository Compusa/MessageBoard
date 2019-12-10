using MessageBoard.Domain.SeedWork;
using System.Threading.Tasks;

namespace MessageBoard.Domain.AggregateModels.MessageAggregate
{
    public interface IMessageRepository : IRepository<Message>
    {
        Message Add(Message message);

        void Update(Message message);

        Task<Message> GetAsync(int messageId);
    }
}
