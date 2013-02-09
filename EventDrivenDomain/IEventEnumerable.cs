namespace EventDrivenDomain
{
    using System;
    using System.Collections.Generic;

    public interface IEventEnumerable<out TBaseCommand> : IEnumerable<TBaseCommand>
    {
        IEventEnumerable<TBaseCommand> SkipForwardTo(DateTime timestamp);
    }
}