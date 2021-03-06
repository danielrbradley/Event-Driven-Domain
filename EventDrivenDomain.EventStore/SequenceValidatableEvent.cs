﻿namespace EventDrivenDomain.EventStore
{
    public sealed class SequenceValidatableEvent<TBaseCommand>
    {
        private readonly Hash previousHash;

        private readonly Event<TBaseCommand> eventResult;

        private readonly Hash hash;

        public SequenceValidatableEvent(Hash previousHash, Event<TBaseCommand> eventResult, Hash hash)
        {
            this.previousHash = previousHash;
            this.eventResult = eventResult;
            this.hash = hash;
        }

        public Hash PreviousHash
        {
            get
            {
                return this.previousHash;
            }
        }

        public Event<TBaseCommand> Event
        {
            get
            {
                return this.eventResult;
            }
        }

        public Hash Hash
        {
            get
            {
                return this.hash;
            }
        }
    }
}