namespace EventDrivenDomain
{
    public interface IEventStore<TBaseCommand>
    {
        Event<TBaseCommand> Write(Message<TBaseCommand> message);
    }
}