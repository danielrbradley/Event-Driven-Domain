namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public class PreviousEventStreamHashReader : IPreviousEventHashReader
    {
        private readonly IPreviousEventStreamProvider previousEventStreamProvider;

        private readonly int hashByteCount;

        public PreviousEventStreamHashReader(IPreviousEventStreamProvider previousEventStreamProvider, int hashByteCount)
        {
            this.previousEventStreamProvider = previousEventStreamProvider;
            this.hashByteCount = hashByteCount;
        }

        public Hash ReadPreviousHash()
        {
            using (var stream = previousEventStreamProvider.GetPreviousEventStream())
            {
                var hashBuffer = new byte[this.hashByteCount];
                stream.Seek(stream.Length - this.hashByteCount, SeekOrigin.Begin);
                stream.Read(hashBuffer, 0, this.hashByteCount);
                var hash = new Hash(hashBuffer);
                return hash;
            }
        }
    }
}
