namespace EventDrivenDomain
{
    using System;

    public interface IEventStoreWriteLock
    {
        IDisposable WaitAquire();
    }
}
