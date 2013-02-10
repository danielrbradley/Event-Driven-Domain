namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IEventStoreEnumerator<TBaseCommand>
    {
        IEnumerable<Event<TBaseCommand>> Events { get; }
    }
}