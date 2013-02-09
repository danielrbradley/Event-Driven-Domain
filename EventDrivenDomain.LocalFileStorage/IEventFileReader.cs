namespace EventDrivenDomain.LocalFileStorage
{
    public interface IEventFileReader<TBaseCommand>
    {
        Event<TBaseCommand> Read(string file);
    }
}