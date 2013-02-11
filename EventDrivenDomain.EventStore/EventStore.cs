namespace EventDrivenDomain.EventStore
{
    using System.Collections.Generic;

    public class EventStore<TBaseCommand> : IEventStore<TBaseCommand>
    {
        private readonly ITimestampProvider timestampProvider;

        private readonly IEventStoreWriter<TBaseCommand> eventStoreWriter;

        private readonly IEnumerable<Event<TBaseCommand>> events;

        public EventStore(ITimestampProvider timestampProvider, IEventStoreWriter<TBaseCommand> eventStoreStoreWriter, IEnumerable<Event<TBaseCommand>> events)
        {
            this.timestampProvider = timestampProvider;
            this.eventStoreWriter = eventStoreStoreWriter;
            this.events = events;
        }

        public Event<TBaseCommand> Write(IMessage<TBaseCommand> message)
        {
            var newEvent = new Event<TBaseCommand>(message, this.timestampProvider.GetTimestamp());
            this.eventStoreWriter.Write(newEvent);
            return newEvent;
        }

        public IEnumerable<Event<TBaseCommand>> Events
        {
            get
            {
                return this.events;
            }
        }
    }
}