namespace EventDrivenDomain
{
    public interface IEventWriter<TBaseCommand>
    {
        void Write(Event<TBaseCommand> eventToWrite);
    }
}