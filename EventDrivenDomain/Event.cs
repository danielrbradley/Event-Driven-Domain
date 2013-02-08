namespace EventDrivenDomain
{
    using System;

    public sealed class Event<T>
    {
        private readonly DateTime timestamp;

        private readonly T action;

        private readonly Guid id;

        public Event(Message<T> message, DateTime timestamp)
        {
            this.id = message.Id;
            this.timestamp = timestamp;
            this.action = message.Action;
        }

        public Guid Id
        {
            get
            {
                return id;
            }
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