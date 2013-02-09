namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IEventReader<TBaseCommand>
    {
        IEnumerable<Event<TBaseCommand>> Events { get; }
    }
}