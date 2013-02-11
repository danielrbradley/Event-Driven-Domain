namespace EventDrivenDomain.EventStore
{
    public interface IEventStore<TBaseCommand> : IWritableEventStore<TBaseCommand>, IEnumerableEventStore<TBaseCommand>
    {
    }
}