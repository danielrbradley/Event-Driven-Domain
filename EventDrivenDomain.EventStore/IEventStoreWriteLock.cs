namespace EventDrivenDomain.EventStore
{
    using System;

    public interface IEventStoreWriteLock
    {
        IDisposable WaitAquire();
    }
}
