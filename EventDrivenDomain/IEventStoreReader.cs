namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IEventStoreReader<TBaseCommand>
    {
        IEnumerable<Event<TBaseCommand>> Events { get; }
    }
}