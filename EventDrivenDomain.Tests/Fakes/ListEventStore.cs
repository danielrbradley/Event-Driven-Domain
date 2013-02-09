namespace EventDrivenDomain.Tests.Fakes
{
    using System;
    using System.Collections.Generic;

    public class ListEventStore<TBaseCommand> : IEventStore<TBaseCommand>
    {
        private readonly LinkedList<Event<TBaseCommand>> events = new LinkedList<Event<TBaseCommand>>();

        public IEventEnumerable<TBaseCommand> Events
        {
            get
            {
                return this.events.AsEventEnumerable();
            }
        }

        public Event<TBaseCommand> Write(Message<TBaseCommand> message)
        {
            var newEvent = new Event<TBaseCommand>(message, DateTime.UtcNow);
            this.events.AddLast(newEvent);
            return newEvent;
        }
    }
}
