namespace EventDrivenDomain
{
    using System;

    public interface ITimestampProvider
    {
        DateTime GetTimestamp();
    }
}