using System.Threading;
using System.Threading.Tasks;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Infrastructure
{
    /// <summary>
    /// Message board DbContext.
    /// </summary>
    public class MessageBoardContext : DbContext, IUnitOfWork
    {
        public MessageBoardContext(DbContextOptions<MessageBoardContext> options)
            : base(options)
        {
        }

        public DbSet<BoardMessage> Messages { get; set; }
    }
}
