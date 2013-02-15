namespace EventDrivenDomain.EventStore
{
    using System;

    public class SystemClockUtcTimestampProvider : ITimestampProvider
    {
        public DateTime GetTimestamp()
        {
            return DateTime.UtcNow;
        }
    }
}
