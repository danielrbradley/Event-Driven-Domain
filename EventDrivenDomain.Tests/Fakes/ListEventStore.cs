namespace EventDrivenDomain.Tests.Fakes
{
    using System;
    using System.Collections.Generic;

    public class ListEventStore<T> : IEventStore<T>
    {
        private readonly LinkedList<Event<T>> events = new LinkedList<Event<T>>();

        public IEnumerable<Event<T>> Events
        {
            get
            {
                return this.events;
            }
        }

        public Event<T> Write(Message<T> message)
        {
            var newEvent = new Event<T>(message, DateTime.UtcNow);
            this.events.AddLast(newEvent);
            return newEvent;
        }
    }
}
