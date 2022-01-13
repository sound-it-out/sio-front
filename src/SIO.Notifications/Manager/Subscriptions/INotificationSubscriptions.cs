using System;
using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;

namespace SIO.Notifications.Manager.Subscriptions
{
    public interface INotificationSubscriptions
    {
        Task ExecuteAsync<TEvent>(INotification<TEvent> @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
        Subject Subscribe(Action<INotificationSubscriptionOptions> options);
        void UnSubscribe(Subject id);
    }
}
