namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    public interface IEventFileWriter<TBaseCommand>
    {
        void Write(string filePath, Event<TBaseCommand> eventToWrite);
    }
}