namespace EventDrivenDomain
{
    using System.Collections.Generic;

    public interface IEventEnumerable<T> : IEnumerable<Event<T>>
    {
    }
}