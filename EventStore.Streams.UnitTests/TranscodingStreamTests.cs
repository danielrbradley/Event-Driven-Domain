namespace EventDrivenDomain.EventStore.Streams.UnitTests
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    using EventDrivenDomain.EventStore.Streams;

    using NUnit.Framework;

    public class TranscodingStreamTests
    {
        [Test]
        public void Given_a_pass_through_transcoder_When_writing_to_the_transcoder_Then_inner_stream_contains_content()
        {
            var passThroughTranscodingStreamFactory = new PassThroughTranscodingStreamFactory();
            using (var innerStream = new MemoryStream())
            using (var passThroughStream = passThroughTranscodingStreamFactory.CreateTrancodingStream(innerStream))
            {
                var writer = new StreamWriter(passThroughStream);
                writer.Write("Test");
                writer.Flush();
                passThroughStream.Flush();

                passThroughStream.Position = 0;
                var reader = new StreamReader(innerStream);
                var actual = reader.ReadToEnd();
                Assert.AreEqual("Test", actual);
            }
        }
    }
}
