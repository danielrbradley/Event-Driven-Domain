namespace EventDrivenDomain.LocalFileStorage
{
    public interface IEventFileWriter<TBaseCommand>
    {
        void Write(string filename, Event<TBaseCommand> eventToWrite);
    }
}