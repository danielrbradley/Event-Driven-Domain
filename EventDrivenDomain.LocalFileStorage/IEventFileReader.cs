namespace EventDrivenDomain.LocalFileStorage
{
    public interface IEventFileReader<TBaseCommand>
    {
        Event<TBaseCommand> Read(EventReadState state, string filePath, out string hash);
    }
}