namespace EventDrivenDomain.EventStore
{
    public interface IMessage<out TBaseCommand>
    {
        TBaseCommand Command { get; }
    }
}