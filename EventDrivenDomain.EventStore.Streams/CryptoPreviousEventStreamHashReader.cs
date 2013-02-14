namespace EventDrivenDomain.EventStore.Streams
{
    public class CryptoPreviousEventStreamHashReader : IPreviousEventHashReader
    {
        private readonly IPreviousEventStreamProvider previousEventStreamProvider;

        private readonly IStreamHashGenerator streamHashGenerator;

        public CryptoPreviousEventStreamHashReader(IPreviousEventStreamProvider previousEventStreamProvider, IStreamHashGenerator streamHashGenerator)
        {
            this.previousEventStreamProvider = previousEventStreamProvider;
            this.streamHashGenerator = streamHashGenerator;
        }

        public Hash ReadPreviousHash()
        {
            using (var stream = previousEventStreamProvider.GetPreviousEventStream())
            {
                var hash = this.streamHashGenerator.GenerateHash(stream);
                return hash;
            }
        }
    }
}
