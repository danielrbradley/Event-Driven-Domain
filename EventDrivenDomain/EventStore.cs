namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public class EventStore<TBaseCommand> : IEventStore<TBaseCommand>
    {
        private readonly ITimestampProvider timestampProvider;

        private readonly IEventWriter<TBaseCommand> eventWriter;

        private readonly IEventReader<TBaseCommand> eventReader;

        public EventStore(ITimestampProvider timestampProvider, IEventReaderWriter<TBaseCommand> eventReaderWriter)
        {
            this.timestampProvider = timestampProvider;
            this.eventWriter = eventReaderWriter;
            this.eventReader = eventReaderWriter;
        }

        public EventStore(ITimestampProvider timestampProvider, IEventWriter<TBaseCommand> eventWriter, IEventReader<TBaseCommand> eventReader)
        {
            this.timestampProvider = timestampProvider;
            this.eventWriter = eventWriter;
            this.eventReader = eventReader;
        }

        public Event<TBaseCommand> Write(Message<TBaseCommand> message)
        {
            var newEvent = new Event<TBaseCommand>(message, this.timestampProvider.GetTimestamp());
            this.eventWriter.Write(newEvent);
            return newEvent;
        }

        public IEnumerable<Event<TBaseCommand>> Events
        {
            get
            {
                return eventReader.Events;
            }
        }
    }
}