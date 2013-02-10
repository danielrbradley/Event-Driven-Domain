namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IEnumerableEventStore<TBaseCommand>
    {
        IEnumerable<Event<TBaseCommand>> Events { get; }
    }
}