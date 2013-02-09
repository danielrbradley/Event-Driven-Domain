namespace EventDrivenDomain
{
    public interface IEventReader<TBaseCommand>
    {
        IEventEnumerable<TBaseCommand> GetEvents();
    }
}