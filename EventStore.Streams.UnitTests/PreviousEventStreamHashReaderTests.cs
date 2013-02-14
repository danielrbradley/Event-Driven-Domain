namespace EventDrivenDomain.EventStore.Streams.UnitTests
{
    using System.IO;

    using EventDrivenDomain.EventStore.Streams.UnitTests.Fakes;

    using NUnit.Framework;

    public class PreviousEventStreamHashReaderTests
    {
        [Test]
        public void Given_a_previous_event_stream_hash_reader_When_reading_a_stream_with_a_four_byte_hash_Then_return_the_last_four_bytes()
        {
            var hash = new Hash(new byte[] { 1, 2, 3, 4 });
            var hashChecksumTranscodingStreamFactory =
                new HashChecksumTranscodingStreamFactory(new StreamHashGenerator(hash));

            using (var stream = new MemoryStream())
            using (var hashChecksumTranscodingStream = hashChecksumTranscodingStreamFactory.CreateTrancodingStream(stream))
            {
                var writer = new StreamWriter(hashChecksumTranscodingStream);
                writer.Write("Test");
                writer.Flush();
                hashChecksumTranscodingStream.Flush();

                var previousEventStreamHashReader =
                    new PreviousEventStreamHashReader(new EventStreamProvider(stream), 4);

                var previousHash = previousEventStreamHashReader.ReadPreviousHash();
                Assert.AreEqual(hash, previousHash);
            }
        }
    }
}
