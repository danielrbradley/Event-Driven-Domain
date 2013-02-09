namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public class EventFileReader<TBaseCommand> : IEventFileReader<TBaseCommand>
    {
        private readonly IEventStreamReader<TBaseCommand> eventStreamReader;

        private readonly int fileStreamBufferSize;

        private readonly FileOptions fileStreamOptions;

        public EventFileReader(IEventStreamReader<TBaseCommand> eventStreamReader, int fileStreamBufferSize, FileOptions fileStreamOptions)
        {
            this.eventStreamReader = eventStreamReader;
            this.fileStreamBufferSize = fileStreamBufferSize;
            this.fileStreamOptions = fileStreamOptions;
        }

        public EventReadResult<TBaseCommand> Read(string filePath)
        {
            using (
                var filestream = new FileStream(
                    filePath, FileMode.Open, FileAccess.Read, FileShare.Read, fileStreamBufferSize, fileStreamOptions))
            {
                return eventStreamReader.Read(filestream);
            }
        }
    }
}