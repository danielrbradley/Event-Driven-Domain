namespace EventDrivenDomain.EventStore
{
    public interface IEventStoreWriter<TBaseCommand>
    {
        void Write(Event<TBaseCommand> eventToWrite);
    }
}