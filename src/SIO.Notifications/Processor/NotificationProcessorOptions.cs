using System;
using System.Collections.Generic;

namespace SIO.Notifications.Processor
{
    public sealed class NotificationProcessorOptions
    {
        private readonly HashSet<Type> _events = new();
        private string _url;
        public IEnumerable<Type> Events => _events;

        public string Url => _url;

        public NotificationProcessorOptions ForEvent<TEvent>()
        {
            _events.Add(typeof(TEvent));
            return this;
        }

        public NotificationProcessorOptions WithUrl(string url)
        {
            _url = url;
            return this;
        }
    }
}
