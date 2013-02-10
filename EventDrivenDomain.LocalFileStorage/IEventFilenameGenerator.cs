namespace EventDrivenDomain.LocalFileStorage
{
    public interface IEventFilenameGenerator
    {
        string CreateFilename<T>(Event<T> eventToWrite);
    }
}