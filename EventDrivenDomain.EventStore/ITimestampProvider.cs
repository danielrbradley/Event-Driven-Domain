namespace EventDrivenDomain.EventStore
{
    using System;

    public interface ITimestampProvider
    {
        DateTime GetTimestamp();
    }
}