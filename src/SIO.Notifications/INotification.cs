using SIO.Infrastructure.Events;

namespace SIO.Notifications
{
    public interface INotification<TEvent> : IEventNotification<TEvent> where TEvent : IEvent
    {
    }
}
