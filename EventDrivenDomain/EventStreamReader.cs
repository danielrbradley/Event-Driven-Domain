namespace EventDrivenDomain
{
    using System.IO;

    public class EventStreamReader<TBaseCommand> : IEventStreamReader<TBaseCommand>
    {
        private readonly IEventDecoder<TBaseCommand> eventDecoder;

        private readonly IHashedStreamReader hashedStreamReader;

        public EventStreamReader(IEventDecoder<TBaseCommand> eventDecoder, IHashedStreamReader hashedStreamReader)
        {
            this.eventDecoder = eventDecoder;
            this.hashedStreamReader = hashedStreamReader;
        }

        public EventReadResult<TBaseCommand> Read(Stream stream)
        {
            using (var contentStream = new MemoryStream())
            {
                string previousHash, expectedStreamHash, actualStreamHash;
                this.hashedStreamReader.Read(stream, contentStream, out previousHash, out expectedStreamHash, out actualStreamHash);
                if (expectedStreamHash != actualStreamHash)
                {
                    throw new EventStoreCorruptionException("Event stream read internal hash mismatch.");
                }

                contentStream.Seek(0, SeekOrigin.Begin);
                var eventResult = this.eventDecoder.ReadEvent(contentStream);
                return new EventReadResult<TBaseCommand>(previousHash, eventResult, actualStreamHash);
            }
        }
    }
}