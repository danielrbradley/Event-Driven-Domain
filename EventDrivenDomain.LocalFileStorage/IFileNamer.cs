namespace EventDrivenDomain.LocalFileStorage
{
    public interface IFileNamer
    {
        string GetFilename<T>(Event<T> eventToWrite);
    }
}