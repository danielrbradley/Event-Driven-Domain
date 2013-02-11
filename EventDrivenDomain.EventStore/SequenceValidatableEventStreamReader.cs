namespace EventDrivenDomain.EventStore
{
    using System.IO;

    public class SequenceValidatableEventStreamReader<TBaseCommand> : ISequenceValidatableEventStreamReader<TBaseCommand>
    {
        private readonly IEventDecoder<TBaseCommand> eventDecoder;

        private readonly IHashedStreamReader hashedStreamReader;

        public SequenceValidatableEventStreamReader(IEventDecoder<TBaseCommand> eventDecoder, IHashedStreamReader hashedStreamReader)
        {
            this.eventDecoder = eventDecoder;
            this.hashedStreamReader = hashedStreamReader;
        }

        public SequenceValidatableEvent<TBaseCommand> Read(Stream stream)
        {
            using (var contentStream = new MemoryStream())
            {
                Hash previousHash, expectedStreamHash, actualStreamHash;
                this.hashedStreamReader.Read(stream, contentStream, out previousHash, out expectedStreamHash, out actualStreamHash);
                if (expectedStreamHash != actualStreamHash)
                {
                    throw new EventStoreCorruptionException("Event stream read internal hash mismatch.");
                }

                contentStream.Seek(0, SeekOrigin.Begin);
                var eventResult = this.eventDecoder.ReadEvent(contentStream);
                return new SequenceValidatableEvent<TBaseCommand>(previousHash, eventResult, actualStreamHash);
            }
        }
    }
}