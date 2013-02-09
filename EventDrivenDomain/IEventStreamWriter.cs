namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamWriter<TBaseCommand>
    {
        void Write(Stream stream, Event<TBaseCommand> eventToWrite);
    }
}