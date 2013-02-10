namespace EventDrivenDomain
{
    using System.Collections;
    using System.Collections.Generic;

    public class SequenceValidatedEventStoreEnumerable<TBaseCommand> : IEnumerable<Event<TBaseCommand>>
    {
        private readonly IEnumerable<SequenceValidatableEvent<TBaseCommand>> sequenceValidatableEventEnumerable;

        public SequenceValidatedEventStoreEnumerable(IEnumerable<SequenceValidatableEvent<TBaseCommand>> sequenceValidatableEventEnumerable)
        {
            this.sequenceValidatableEventEnumerable = sequenceValidatableEventEnumerable;
        }

        public IEnumerable<Event<TBaseCommand>> Enumerable
        {
            get
            {
                Hash previousHash = Hash.None;
                bool isFirst = true;
                foreach (var eventReadResult in this.sequenceValidatableEventEnumerable)
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

        public IEnumerator<Event<TBaseCommand>> GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}