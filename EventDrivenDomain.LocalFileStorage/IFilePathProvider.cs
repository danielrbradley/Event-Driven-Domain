namespace EventDrivenDomain.LocalFileStorage
{
    public interface IFilePathProvider
    {
        string GetFilePath<T>(Event<T> eventToWrite);
    }
}