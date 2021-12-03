using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Queries
{
    internal sealed class NoOpQueryStore : IQueryStore
    {
        public Task SaveAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
