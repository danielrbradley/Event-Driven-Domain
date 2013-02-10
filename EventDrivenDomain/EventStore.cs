namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public class EventStore<TBaseCommand> : IEventStore<TBaseCommand>
    {
        private readonly ITimestampProvider timestampProvider;

        private readonly IEventStoreWriter<TBaseCommand> eventStoreWriter;

        private readonly IEventStoreEnumerator<TBaseCommand> eventStoreEnumerator;

        public EventStore(ITimestampProvider timestampProvider, IEventStoreWriter<TBaseCommand> eventStoreStoreWriter, IEventStoreEnumerator<TBaseCommand> eventStoreEnumerator)
        {
            this.timestampProvider = timestampProvider;
            this.eventStoreWriter = eventStoreStoreWriter;
            this.eventStoreEnumerator = eventStoreEnumerator;
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
                return this.eventStoreEnumerator.Events;
            }
        }
    }
}