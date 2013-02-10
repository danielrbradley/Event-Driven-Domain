namespace EventDrivenDomain
{
    using System.Security.Policy;

    public sealed class EventReadResult<TBaseCommand>
    {
        private readonly Hash previousHash;

        private readonly Event<TBaseCommand> eventResult;

        private readonly Hash hash;

        public EventReadResult(Hash previousHash, Event<TBaseCommand> eventResult, Hash hash)
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