namespace EventDrivenDomain
{
    public interface IWritableEventStore<TBaseCommand>
    {
        Event<TBaseCommand> Write(IMessage<TBaseCommand> message);
    }
}