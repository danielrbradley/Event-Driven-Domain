namespace EventDrivenDomain.LocalFileStorage
{
    public interface IEventFileWriter<TBaseCommand>
    {
        void Write(string filePath, Event<TBaseCommand> eventToWrite);
    }
}