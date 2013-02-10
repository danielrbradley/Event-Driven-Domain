namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IEventStoreReader<TBaseCommand>
    {
        IEnumerable<EventReadResult<TBaseCommand>> EventReadResults { get; }
    }
}