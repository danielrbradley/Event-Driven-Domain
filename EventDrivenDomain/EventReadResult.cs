namespace EventDrivenDomain
{
    public sealed class EventReadResult<TBaseCommand>
    {
        private readonly string previousHash;

        private readonly Event<TBaseCommand> eventResult;

        private readonly string hash;

        public EventReadResult(string previousHash, Event<TBaseCommand> eventResult, string hash)
        {
            this.previousHash = previousHash;
            this.eventResult = eventResult;
            this.hash = hash;
        }

        public string PreviousHash
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

        public string Hash
        {
            get
            {
                return this.hash;
            }
        }
    }
}