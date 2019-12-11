using System;
using System.Threading.Tasks;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageBoardContext _context;

        public MessageRepository(MessageBoardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Message Add(Message message)
        {
            return _context.Add(message).Entity;
        }

        public void Update(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
        }

        public void Remove(Message message)
        {
            _context.Entry(message).State = EntityState.Deleted;
        }

        public async Task<Message> GetAsync(int messageId)
        {
            return await _context.Messages.FindAsync(messageId);
        }
    }
}
