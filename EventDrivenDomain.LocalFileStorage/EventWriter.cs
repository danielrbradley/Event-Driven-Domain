namespace EventDrivenDomain.LocalFileStorage
{
    public class EventWriter<TBaseCommand> : IEventWriter<TBaseCommand>
    {
        private readonly IFileNamer fileNamer;

        private readonly IEventFileWriter<TBaseCommand> eventFileWriter;

        public EventWriter(IFileNamer fileNamer, IEventFileWriter<TBaseCommand> eventFileWriter)
        {
            this.fileNamer = fileNamer;
            this.eventFileWriter = eventFileWriter;
        }

        public void Write(Event<TBaseCommand> eventToWrite)
        {
            var filename = this.fileNamer.GetFilename(eventToWrite);
            this.eventFileWriter.Write(filename, eventToWrite);
        }
    }
}