namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    using System.IO;

    public class SequenceValidatableEventFileReader<TBaseCommand> : ISequenceValidatableEventFileReader<TBaseCommand>
    {
        private readonly ISequenceValidatableEventStreamReader<TBaseCommand> sequenceValidatableEventStreamReader;

        private readonly int fileStreamBufferSize;

        private readonly FileOptions fileStreamOptions;

        public SequenceValidatableEventFileReader(ISequenceValidatableEventStreamReader<TBaseCommand> sequenceValidatableEventStreamReader, int fileStreamBufferSize, FileOptions fileStreamOptions)
        {
            this.sequenceValidatableEventStreamReader = sequenceValidatableEventStreamReader;
            this.fileStreamBufferSize = fileStreamBufferSize;
            this.fileStreamOptions = fileStreamOptions;
        }

        public SequenceValidatableEvent<TBaseCommand> Read(string filePath)
        {
            using (
                var filestream = new FileStream(
                    filePath, FileMode.Open, FileAccess.Read, FileShare.Read, fileStreamBufferSize, fileStreamOptions))
            {
                return this.sequenceValidatableEventStreamReader.Read(filestream);
            }
        }
    }
}