namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    using System.IO;

    public class EventFileWriter<TBaseCommand> : IEventFileWriter<TBaseCommand>
    {
        private readonly IStreamEventWriter<TBaseCommand> streamEventWriter;

        private readonly int bufferSize;

        public EventFileWriter(IStreamEventWriter<TBaseCommand> streamEventWriter, int bufferSize)
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
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize,
                    FileOptions.SequentialScan))
            {
                this.streamEventWriter.Write(filestream, eventToWrite);
            }
        }
    }
}