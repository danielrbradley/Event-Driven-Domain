namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IReadableEventStore<TBaseCommand>
    {
        IEnumerable<Event<TBaseCommand>> Events { get; }
    }
}