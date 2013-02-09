namespace EventDrivenDomain
{
    public interface ILatestEventReader<TBaseCommand>
    {
        Event<TBaseCommand> LatestEvent { get; }
    }
}