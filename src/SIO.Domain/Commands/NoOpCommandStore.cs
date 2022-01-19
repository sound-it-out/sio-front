using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Commands
{
    internal sealed class NoOpCommandStore : ICommandStore
    {
        public Task SaveAsync(ICommand command, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
