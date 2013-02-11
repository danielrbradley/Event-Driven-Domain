namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    public interface IEventFilenameGenerator
    {
        string CreateFilename<T>(Event<T> eventToWrite);
    }
}