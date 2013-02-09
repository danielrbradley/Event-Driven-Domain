namespace EventDrivenDomain.LocalFileStorage
{
    public interface IEventFileReader<TBaseCommand>
    {
        EventReadResult<TBaseCommand> Read(string filePath);
    }
}