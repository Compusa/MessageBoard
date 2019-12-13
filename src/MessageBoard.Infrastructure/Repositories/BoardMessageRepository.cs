using System;
using System.Threading.Tasks;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Infrastructure.Repositories
{
    public class BoardMessageRepository : IBoardMessageRepository
    {
        private readonly MessageBoardContext _context;

        public BoardMessageRepository(MessageBoardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public BoardMessage Add(BoardMessage message)
        {
            return _context.Add(message).Entity;
        }

        public void Update(BoardMessage message)
        {
            _context.Entry(message).State = EntityState.Modified;
        }

        public void Remove(BoardMessage message)
        {
            _context.Entry(message).State = EntityState.Deleted;
        }

        public async Task<BoardMessage> GetAsync(int messageId)
        {
            return await _context.Messages.FindAsync(messageId);
        }
    }
}
