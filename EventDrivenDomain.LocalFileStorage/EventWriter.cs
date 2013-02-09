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
            var filePath = this.fileNamer.GetFilePath(eventToWrite);
            this.eventFileWriter.Write(filePath, eventToWrite);
        }
    }
}