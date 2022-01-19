using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;

namespace SIO.Notifications.Manager.Subscriptions
{
    internal sealed class NotificationSubscriptions : INotificationSubscriptions
    {
        private readonly Dictionary<Subject, IDictionary<Type, IEnumerable<Func<object, CancellationToken, Task>>>> _subscriptions;

        public NotificationSubscriptions()
        {
            _subscriptions = new();
        }

        public async Task ExecuteAsync<TEvent>(INotification<TEvent> @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            var type = typeof(TEvent);
            await Task.WhenAll(_subscriptions.Values.Select(s => ExecuteSubscriptionAsync(type, (object)@event, s, cancellationToken)));
        }

        private async Task ExecuteSubscriptionAsync(
            Type type,
            object @event,
            IDictionary<Type, IEnumerable<Func<object, CancellationToken, Task>>> subscription,
            CancellationToken cancellationToken = default)
        {
            if (subscription.TryGetValue(type, out var handlers))
                await Task.WhenAll(handlers.Select(h => h(@event, cancellationToken)));
        }

        public Subject Subscribe(Action<INotificationSubscriptionOptions> options)
        {
            var subscription = new NotificationSubscriptionOptions();
            options(subscription);

            if (!_subscriptions.TryGetValue(subscription.Id, out var configuration))
                configuration = new Dictionary<Type, IEnumerable<Func<object, CancellationToken, Task>>>();

            foreach (var @event in subscription.Events)
            {
                if (!configuration.TryGetValue(@event, out var handlers))
                    handlers = Enumerable.Empty<Func<object, CancellationToken, Task>>();

                handlers = handlers.Concat(subscription.Handlers);

                configuration[@event] = handlers;
            }

            _subscriptions[subscription.Id] = configuration;

            return subscription.Id;
        }

        public void UnSubscribe(Subject id) => _subscriptions.Remove(id, out _);
    }
}
