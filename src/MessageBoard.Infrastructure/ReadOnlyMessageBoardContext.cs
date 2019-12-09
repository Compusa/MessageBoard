using MessageBoard.Domain;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.Infrastructure
{
    public class ReadOnlyMessageBoardContext : IReadOnlyMessageBoardContext, IDisposable
    {
        private bool _disposed;

        private readonly MessageBoardContext _context;

        public ReadOnlyMessageBoardContext(MessageBoardContext context)
        {
            _context = context;
        }

        public IQueryable<Message> Messages => _context.Messages.AsNoTracking();

        public ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            return _context.FindAsync<TEntity>(keyValues);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _context.Dispose();

            _disposed = true;
        }
    }
}
