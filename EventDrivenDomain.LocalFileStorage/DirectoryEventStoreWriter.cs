namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public class DirectoryEventStoreWriter<TBaseCommand> : IEventStoreWriter<TBaseCommand>
    {
        private readonly string path;

        private readonly IEventFilenameGenerator filePathProvider;

        private readonly IEventFileWriter<TBaseCommand> eventFileWriter;

        public DirectoryEventStoreWriter(string path, IEventFilenameGenerator filePathProvider, IEventFileWriter<TBaseCommand> eventFileWriter)
        {
            this.path = path;
            this.filePathProvider = filePathProvider;
            this.eventFileWriter = eventFileWriter;
        }

        public void Write(Event<TBaseCommand> eventToWrite)
        {
            var filename = this.filePathProvider.CreateFilename(eventToWrite);
            var filePath = Path.Combine(this.path, filename);
            this.eventFileWriter.Write(filePath, eventToWrite);
        }
    }
}