namespace EventDrivenDomain
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class EventStoreCorruptionException : Exception
    {
        public EventStoreCorruptionException()
        {
        }

        public EventStoreCorruptionException(string message)
            : base(message)
        {
        }

        public EventStoreCorruptionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected EventStoreCorruptionException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}