namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamReader<TBaseCommand>
    {
        Event<TBaseCommand> Read(EventReadState state, Stream stream, out string hash);
    }
}