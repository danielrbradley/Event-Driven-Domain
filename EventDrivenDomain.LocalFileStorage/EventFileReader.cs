namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public class EventFileReader<TBaseCommand> : IEventFileReader<TBaseCommand>
    {
        private readonly IEventStreamReader<TBaseCommand> eventStreamReader;

        private readonly int bufferSize;

        public EventFileReader(IEventStreamReader<TBaseCommand> eventStreamReader, int bufferSize)
        {
            this.eventStreamReader = eventStreamReader;
            this.bufferSize = bufferSize;
        }

        public Event<TBaseCommand> Read(string filePath)
        {
            using (
                var filestream = new FileStream(
                    filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
            {
                return eventStreamReader.Read(filestream);
            }
        }
    }
}