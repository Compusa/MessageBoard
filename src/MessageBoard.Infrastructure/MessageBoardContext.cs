using System.Threading;
using System.Threading.Tasks;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Infrastructure
{
    public class MessageBoardContext : DbContext, IUnitOfWork
    {
        public MessageBoardContext(DbContextOptions<MessageBoardContext> options)
            : base(options)
        {
        }

        public DbSet<BoardMessage> Messages { get; set; }
    }
}
