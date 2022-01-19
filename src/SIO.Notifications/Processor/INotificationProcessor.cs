using System.Threading;
using System.Threading.Tasks;

namespace SIO.Notifications.Processor
{
    public interface INotificationProcessor
    {
        Task StartAsync(CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
