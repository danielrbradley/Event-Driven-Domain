namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventDecoder<TBaseCommand>
    {
        Event<TBaseCommand> ReadEvent(Stream stream);
    }
}