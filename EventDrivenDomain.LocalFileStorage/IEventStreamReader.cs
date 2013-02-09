namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public interface IEventStreamReader<TBaseCommand>
    {
        Event<TBaseCommand> Read(Stream stream);
    }
}