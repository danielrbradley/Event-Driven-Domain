namespace EventDrivenDomain.EventStore
{
    using System.Collections.Generic;

    public interface IEnumerableEventStore<TBaseCommand>
    {
        IEnumerable<Event<TBaseCommand>> Events { get; }
    }
}