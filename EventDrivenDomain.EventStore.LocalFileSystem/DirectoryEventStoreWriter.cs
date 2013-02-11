namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    using System.IO;

    public class DirectoryEventStoreWriter<TBaseCommand> : IEventStoreWriter<TBaseCommand>
    {
        private readonly string path;

        private readonly IEventFilenameGenerator filePathProvider;

        private readonly IEventStoreWriteLock writeLock;

        private readonly IEventFileWriter<TBaseCommand> eventFileWriter;

        public DirectoryEventStoreWriter(string path, IEventFilenameGenerator filePathProvider, IEventStoreWriteLock writeLock, IEventFileWriter<TBaseCommand> eventFileWriter)
        {
            this.path = path;
            this.filePathProvider = filePathProvider;
            this.writeLock = writeLock;
            this.eventFileWriter = eventFileWriter;
        }

        public void Write(Event<TBaseCommand> eventToWrite)
        {
            var filename = this.filePathProvider.CreateFilename(eventToWrite);
            var filePath = Path.Combine(this.path, filename);
            using (writeLock.WaitAquire())
            {
                this.eventFileWriter.Write(filePath, eventToWrite);
            }
        }
    }
}