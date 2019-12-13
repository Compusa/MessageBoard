using MessageBoard.Domain.AggregateModels.MessageAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.Domain
{

    public interface IReadOnlyMessageBoardContext
    {
        IQueryable<BoardMessage> Messages { get; }

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database. Otherwise, a query is made to the database
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to find.</typeparam>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
    }
}
