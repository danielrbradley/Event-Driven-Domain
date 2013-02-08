namespace EventDrivenDomain
{
    using System;

    public sealed class Event<T>
    {
        private readonly DateTime timestamp;

        private readonly T action;

        public Event(DateTime timestamp, T action)
        {
            this.timestamp = timestamp;
            this.action = action;
        }

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
        }

        public T Action
        {
            get
            {
                return action;
            }
        }
    }
}