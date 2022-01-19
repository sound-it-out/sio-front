using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;

namespace SIO.Notifications.Manager.Subscriptions
{
    internal class NotificationSubscriptionOptions : INotificationSubscriptionOptions, INotificationActionOptions, INotificationHandlerOptions
    {        
        private readonly List<Type> _events;
        private readonly List<Func<object, CancellationToken, Task>> _handlers;

        public Subject Id { get; }
        public IEnumerable<Func<object, CancellationToken, Task>> Handlers => _handlers;
        public IEnumerable<Type> Events => _events;

        public NotificationSubscriptionOptions()
        {
            Id = Subject.New();
            _events = new();
            _handlers = new();
        }

        public INotificationHandlerOptions For<TEvent>() where TEvent : IEvent
        {
            _events.Add(typeof(TEvent));
            return this;
        }

        public INotificationActionOptions With<TEvent>(Func<INotification<TEvent>, CancellationToken, Task> handler)
             where TEvent : IEvent
        {
            _handlers.Add((notification, token) => handler((INotification<TEvent>)notification, token));
            return this;
        }
    }
}
