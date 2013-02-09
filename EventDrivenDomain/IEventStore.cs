namespace EventDrivenDomain
{
    public interface IEventStore<TBaseCommand> : IWritableEventStore<TBaseCommand>, IReadableEventStore<TBaseCommand>
    {
    }
}