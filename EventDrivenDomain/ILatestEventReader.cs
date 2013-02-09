namespace EventDrivenDomain
{
    public interface ILatestEventReader<TBaseCommand>
    {
        Event<TBaseCommand> PreviousEvent { get; }
    }
}