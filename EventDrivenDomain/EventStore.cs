namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public class EventStore<TBaseCommand> : IEventStore<TBaseCommand>
    {
        private readonly ITimestampProvider timestampProvider;

        private readonly IEventStoreWriter<TBaseCommand> eventStoreWriter;

        private readonly IEventStoreReader<TBaseCommand> eventStoreReader;

        public EventStore(ITimestampProvider timestampProvider, IEventStoreReaderWriter<TBaseCommand> eventStoreReaderWriter)
        {
            this.timestampProvider = timestampProvider;
            this.eventStoreWriter = eventStoreReaderWriter;
            this.eventStoreReader = eventStoreReaderWriter;
        }

        public EventStore(ITimestampProvider timestampProvider, IEventStoreWriter<TBaseCommand> eventStoreStoreWriter, IEventStoreReader<TBaseCommand> eventStoreStoreReader)
        {
            this.timestampProvider = timestampProvider;
            this.eventStoreWriter = eventStoreStoreWriter;
            this.eventStoreReader = eventStoreStoreReader;
        }

        public Event<TBaseCommand> Write(Message<TBaseCommand> message)
        {
            var newEvent = new Event<TBaseCommand>(message, this.timestampProvider.GetTimestamp());
            this.eventStoreWriter.Write(newEvent);
            return newEvent;
        }

        public IEnumerable<Event<TBaseCommand>> Events
        {
            get
            {
                return this.eventStoreReader.Events;
            }
        }
    }
}