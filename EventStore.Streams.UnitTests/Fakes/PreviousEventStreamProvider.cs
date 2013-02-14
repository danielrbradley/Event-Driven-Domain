namespace EventDrivenDomain.EventStore.Streams.UnitTests.Fakes
{
    using System.IO;

    public class PreviousEventStreamProvider : IPreviousEventStreamProvider
    {
        private readonly Stream stream;

        public PreviousEventStreamProvider(Stream stream)
        {
            this.stream = stream;
        }

        public Stream GetPreviousEventStream()
        {
            return this.stream;
        }
    }
}
