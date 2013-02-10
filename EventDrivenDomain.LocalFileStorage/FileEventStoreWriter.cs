namespace EventDrivenDomain.LocalFileStorage
{
    public class FileEventStoreWriter<TBaseCommand> : IEventStoreWriter<TBaseCommand>
    {
        private readonly IFilePathProvider filePathProvider;

        private readonly IEventFileWriter<TBaseCommand> eventFileWriter;

        public FileEventStoreWriter(IFilePathProvider filePathProvider, IEventFileWriter<TBaseCommand> eventFileWriter)
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