namespace EventDrivenDomain.EventStore.Tests.Fakes
{
    using System;
    using System.Collections.Generic;

    public class ListEventStore<TBaseCommand> : IEventStore<TBaseCommand>
    {
        private readonly LinkedList<Event<TBaseCommand>> events = new LinkedList<Event<TBaseCommand>>();

        public IEnumerable<Event<TBaseCommand>> Events
        {
            get
            {
                return this.events;
            }
        }

        public Event<TBaseCommand> Write(IMessage<TBaseCommand> message)
        {
            var newEvent = new Event<TBaseCommand>(message, DateTime.UtcNow);
            this.events.AddLast(newEvent);
            return newEvent;
        }
    }
}
