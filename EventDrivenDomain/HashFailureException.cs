namespace EventDrivenDomain
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class HashFailureException : Exception
    {
        public HashFailureException()
        {
        }

        public HashFailureException(string message)
            : base(message)
        {
        }

        public HashFailureException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected HashFailureException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
