using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;
using SIO.Notifications.Manager.Subscriptions;

namespace SIO.Notifications.Manager
{
    internal sealed class NotificationManager : INotificationManager
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly INotificationSubscriptions _notificationSubscriptions;
        private readonly ILogger<NotificationManager> _logger;

        public NotificationManager(IEventDispatcher eventDispatcher,
            INotificationSubscriptions notificationSubscriptions,
            ILogger<NotificationManager> logger)
        {
            if (eventDispatcher == null)
                throw new ArgumentNullException(nameof(eventDispatcher));
            if (notificationSubscriptions == null)
                throw new ArgumentNullException(nameof(notificationSubscriptions));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _eventDispatcher = eventDispatcher;
            _notificationSubscriptions = notificationSubscriptions;
            _logger = logger;
        }

        public async Task ExecuteAsync<TEvent>(Notification<TEvent> @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            _logger.LogInformation($"{nameof(NotificationManager)} - {typeof(TEvent).Name} being executed");
            await _eventDispatcher.DispatchAsync(new EventContext<TEvent>(@event.StreamId, @event.Payload, @event.CorrelationId, @event.CausationId, @event.Timestamp, Actor.From(@event.UserId)), cancellationToken);
            _logger.LogInformation($"{nameof(NotificationManager)} - {typeof(TEvent).Name} handler executed");
            try
            {
                await _notificationSubscriptions.ExecuteAsync(@event, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }            
        }

        public Subject Subscribe(Action<INotificationSubscriptionOptions> options) => _notificationSubscriptions.Subscribe(options);
        public void UnSubscribe(Subject id) => _notificationSubscriptions.UnSubscribe(id);
    }
}
