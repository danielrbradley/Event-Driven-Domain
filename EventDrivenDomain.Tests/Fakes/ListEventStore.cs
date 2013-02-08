namespace EventDrivenDomain.Tests.Fakes
{
    using System;
    using System.Collections.Generic;

    public class ListEventStore<T> : IEventStore<T>
    {
        private readonly LinkedList<T> events = new LinkedList<T>();

        public IEnumerable<T> Events
        {
            get
            {
                return this.events;
            }
        }

        public Event<T> Write(T action)
        {
            this.events.AddLast(action);
            return new Event<T>(DateTime.UtcNow, action);
        }
    }
}
