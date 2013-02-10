namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public class EventStoreEnumerator<TBaseCommand> : IEventStoreEnumerator<TBaseCommand>
    {
        private readonly IEventStoreReader<TBaseCommand> eventStoreReader;

        public EventStoreEnumerator(IEventStoreReader<TBaseCommand> eventStoreReader)
        {
            this.eventStoreReader = eventStoreReader;
        }

        public IEnumerable<Event<TBaseCommand>> Events
        {
            get
            {
                Hash previousHash = Hash.None;
                bool isFirst = true;
                foreach (var eventReadResult in this.eventStoreReader.EventReadResults)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else if (previousHash != eventReadResult.PreviousHash)
                    {
                        throw new EventStoreCorruptionException("Event sequence hash mismatch");
                    }

                    yield return eventReadResult.Event;

                    previousHash = eventReadResult.Hash;
                }
            }
        }
    }
}