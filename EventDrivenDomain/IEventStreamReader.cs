namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamReader<TBaseCommand>
    {
        EventReadResult<TBaseCommand> Read(Stream stream);
    }
}