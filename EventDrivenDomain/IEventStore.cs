namespace EventDrivenDomain
{
    public interface IEventStore<TBaseCommand> : IEventWriter<TBaseCommand>
    {
        IEventEnumerable<TBaseCommand> Events { get; }
    }
}