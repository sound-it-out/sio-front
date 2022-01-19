using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;

namespace SIO.Notifications.Manager.Subscriptions
{
    public interface INotificationSubscriptionOptions 
    {
        Subject Id { get; }
        IEnumerable<Func<object, CancellationToken, Task>> Handlers { get; }
        IEnumerable<Type> Events { get; }
        INotificationActionOptions With<TEvent>(Func<INotification<TEvent>, CancellationToken, Task> handler) where TEvent : IEvent;
    }

    public interface INotificationActionOptions
    {
        INotificationHandlerOptions For<TEvent>() where TEvent : IEvent;
    }

    public interface INotificationHandlerOptions
    {
        INotificationHandlerOptions For<TEvent>() where TEvent : IEvent;
    }
}
