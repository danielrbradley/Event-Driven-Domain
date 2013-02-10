namespace EventDrivenDomain
{
    public interface IReadableEventStore<TBaseCommand> : IEventStoreEnumerator<TBaseCommand>
    {
    }
}