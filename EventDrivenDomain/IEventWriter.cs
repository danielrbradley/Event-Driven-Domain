namespace EventDrivenDomain
{
    public interface IEventWriter<TBaseCommand>
    {
        Event<TBaseCommand> Write(Message<TBaseCommand> message);
    }
}