namespace EventDrivenDomain
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidEventStreamException : Exception
    {
        public InvalidEventStreamException()
        {
        }

        public InvalidEventStreamException(string message)
            : base(message)
        {
        }

        public InvalidEventStreamException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidEventStreamException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}