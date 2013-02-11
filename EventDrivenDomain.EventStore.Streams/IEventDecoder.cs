namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IEventDecoder<TBaseCommand>
    {
        Event<TBaseCommand> ReadEvent(Stream stream);
    }
}