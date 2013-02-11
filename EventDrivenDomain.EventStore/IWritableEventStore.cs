namespace EventDrivenDomain.EventStore
{
    public interface IWritableEventStore<TBaseCommand>
    {
        Event<TBaseCommand> Write(IMessage<TBaseCommand> message);
    }
}