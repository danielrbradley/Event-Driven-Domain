namespace EventDrivenDomain
{
    public interface IEventStore<TBaseCommand> : IWritableEventStore<TBaseCommand>, IEnumerableEventStore<TBaseCommand>
    {
    }
}