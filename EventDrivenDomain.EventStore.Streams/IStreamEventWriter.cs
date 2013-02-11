namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IStreamEventWriter<TBaseCommand>
    {
        void Write(Stream stream, Event<TBaseCommand> eventToWrite);
    }
}