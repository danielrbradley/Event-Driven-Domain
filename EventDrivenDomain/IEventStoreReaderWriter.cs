namespace EventDrivenDomain
{
    public interface IEventStoreReaderWriter<TBaseCommand> : IEventStoreReader<TBaseCommand>, IEventStoreWriter<TBaseCommand>
    {
    }
}