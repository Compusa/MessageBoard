using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Infrastructure
{
    public class MessageBoardContext : DbContext
    {
        public MessageBoardContext(DbContextOptions<MessageBoardContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
