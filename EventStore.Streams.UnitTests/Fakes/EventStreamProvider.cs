namespace EventDrivenDomain.EventStore.Streams.UnitTests.Fakes
{
    using System.IO;

    public class EventStreamProvider : IEventStreamProvider
    {
        private readonly Stream stream;

        public EventStreamProvider(Stream stream)
        {
            this.stream = stream;
        }

        public Stream GetPreviousEventStream()
        {
            return this.stream;
        }
    }
}
