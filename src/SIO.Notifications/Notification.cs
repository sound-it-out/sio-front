using System;
using SIO.Infrastructure;
using SIO.Infrastructure.Events;

namespace SIO.Notifications
{
    public sealed record Notification<TEvent>(string StreamId, CorrelationId? CorrelationId, CausationId? CausationId, TEvent Payload, DateTimeOffset Timestamp, string UserId) : INotification<TEvent>
        where TEvent : IEvent;
}
