namespace EventDrivenDomain
{
    public interface IReadableEventStore<TBaseCommand>
    {
        IEventEnumerable<TBaseCommand> Events { get; }
    }
}