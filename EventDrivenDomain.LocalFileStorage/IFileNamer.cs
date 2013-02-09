namespace EventDrivenDomain.LocalFileStorage
{
    public interface IFileNamer
    {
        string GetFilePath<T>(Event<T> eventToWrite);
    }
}