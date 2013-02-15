namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    using System.IO;

    using EventDrivenDomain.EventStore.Streams;

    public class EventFileWriter<TBaseCommand> : IEventFileWriter<TBaseCommand>
    {
        private readonly IStreamEventWriter<TBaseCommand> streamEventWriter;

        private readonly int bufferSize;

        public const int DefaultBufferSize = 4096;

        public EventFileWriter(IStreamEventWriter<TBaseCommand> streamEventWriter, int bufferSize = DefaultBufferSize)
        {
            this.streamEventWriter = streamEventWriter;
            this.bufferSize = bufferSize;
        }

        public void Write(string filePath, Event<TBaseCommand> eventToWrite)
        {
            using (
                var filestream = new FileStream(
                    filePath,
                    FileMode.CreateNew,
                    FileAccess.ReadWrite,
                    FileShare.None,
                    bufferSize,
                    FileOptions.None))
            {
                this.streamEventWriter.Write(filestream, eventToWrite);
                filestream.Flush();
            }
        }
    }
}