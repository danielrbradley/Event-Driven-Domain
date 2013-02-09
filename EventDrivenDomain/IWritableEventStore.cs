namespace EventDrivenDomain
{
    public interface IWritableEventStore<TBaseCommand>
    {
        Event<TBaseCommand> Write(Message<TBaseCommand> message);
    }
}