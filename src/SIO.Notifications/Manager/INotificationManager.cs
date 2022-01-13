using System;
using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;
using SIO.Notifications.Manager.Subscriptions;

namespace SIO.Notifications.Manager
{
    public interface INotificationManager
    {
        Task ExecuteAsync<TEvent>(Notification<TEvent> @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
        Subject Subscribe(Action<INotificationSubscriptionOptions> builder);
        void UnSubscribe(Subject id);
    }
}
