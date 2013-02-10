namespace EventDrivenDomain
{
    public interface IEventStoreWriter<TBaseCommand>
    {
        void Write(Event<TBaseCommand> eventToWrite);
    }
}