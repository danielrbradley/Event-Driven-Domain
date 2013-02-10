namespace EventDrivenDomain
{
    public interface IMessage<out TBaseCommand>
    {
        TBaseCommand Command { get; }
    }
}