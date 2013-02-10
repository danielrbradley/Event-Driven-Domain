namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public class EventFileWriter<TBaseCommand> : IEventFileWriter<TBaseCommand>
    {
        private readonly IEventStreamWriter<TBaseCommand> eventStreamWriter;

        private readonly int bufferSize;

        public EventFileWriter(IEventStreamWriter<TBaseCommand> eventStreamWriter, int bufferSize)
        {
            this.eventStreamWriter = eventStreamWriter;
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
                eventStreamWriter.Write(filestream, eventToWrite);
            }
        }
    }
}