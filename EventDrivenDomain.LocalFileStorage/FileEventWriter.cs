namespace EventDrivenDomain.LocalFileStorage
{
    public class FileEventWriter<TBaseCommand> : IEventWriter<TBaseCommand>
    {
        private readonly IFilePathProvider filePathProvider;

        private readonly IEventFileWriter<TBaseCommand> eventFileWriter;

        public FileEventWriter(IFilePathProvider filePathProvider, IEventFileWriter<TBaseCommand> eventFileWriter)
        {
            this.filePathProvider = filePathProvider;
            this.eventFileWriter = eventFileWriter;
        }

        public void Write(Event<TBaseCommand> eventToWrite)
        {
            var filePath = this.filePathProvider.GetFilePath(eventToWrite);
            this.eventFileWriter.Write(filePath, eventToWrite);
        }
    }
}