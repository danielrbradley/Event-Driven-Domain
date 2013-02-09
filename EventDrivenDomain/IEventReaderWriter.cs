namespace EventDrivenDomain
{
    public interface IEventReaderWriter<TBaseCommand> : IEventReader<TBaseCommand>, IEventWriter<TBaseCommand>
    {
    }
}