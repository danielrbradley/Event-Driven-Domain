namespace EventDrivenDomain.EventStore
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class WriteLockAcquisitionException : Exception
    {
        public WriteLockAcquisitionException()
        {
        }

        public WriteLockAcquisitionException(string message)
            : base(message)
        {
        }

        public WriteLockAcquisitionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WriteLockAcquisitionException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}