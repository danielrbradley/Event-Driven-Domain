namespace EventDrivenDomain.EventStore.Streams.UnitTests
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    using EventDrivenDomain.EventStore.Streams;
    using EventDrivenDomain.EventStore.Streams.UnitTests.Fakes;

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

        [Test]
        public void Given_a_hash_checksum_transcoder_When_written_to_the_transcoder_Then_the_inner_stream_contains_the_written_data_followed_by_its_hash()
        {
            var hashAlgorithm = SHA256.Create();
            var streamHashGenerator = new CryptoStreamHashGenerator(hashAlgorithm);
            var hashChecksumTranscodingStreamFactory =
                new HashChecksumTranscodingStreamFactory(streamHashGenerator);

            const int HashByteCount = 32;
            const string TestText = "Test";

            using (var innerStream = new MemoryStream())
            using (var hashChecksumTranscodingStream = hashChecksumTranscodingStreamFactory.CreateTrancodingStream(innerStream))
            {
                var writer = new StreamWriter(hashChecksumTranscodingStream);
                writer.Write(TestText);
                writer.Flush();
                hashChecksumTranscodingStream.Flush();

                innerStream.Position = 0;
                var reader = new StreamReader(innerStream);
                var stringBuffer = new char[4];
                reader.ReadBlock(stringBuffer, 0, 4);

                var expectedInnerStreamLength = TestText.Length + HashByteCount;
                Assert.AreEqual(expectedInnerStreamLength, innerStream.Length);

                const string ExpectedString = TestText;
                var actualString = new string(stringBuffer);
                Assert.AreEqual(ExpectedString, actualString);

                const string ExpectedHash =
                    "53-2E-AA-BD-95-74-88-0D-BF-76-B9-B8-CC-00-83-2C-20-A6-EC-11-3D-68-22-99-55-0D-7A-6E-0F-34-5E-25";
                innerStream.Position = 4;
                var hashBuffer = new byte[innerStream.Length - innerStream.Position];
                innerStream.Read(hashBuffer, 0, HashByteCount);
                var actualHash = BitConverter.ToString(hashBuffer);
                Assert.AreEqual(ExpectedHash, actualHash);
            }
        }

        [Test]
        public void
            Given_a_sequence_validation_transcoder_When_writen_to_the_transcoding_stream_Then_the_inner_stream_contains_the_previous_identifier_followed_by_the_data_written()
        {
            byte[] hashBytes = { 1, 2, 3, 4 };
            const string TestText = "Test";

            var hash = new Hash(hashBytes);
            var previousEventHashReader = new PreviousEventHashReader(hash);
            var sequenceValidationTranscodingStreamFactory = new SequenceValidationTranscodingStreamFactory(previousEventHashReader);

            using (var innerStream = new MemoryStream())
            using (var sequenceValidationTranscodingStream = sequenceValidationTranscodingStreamFactory.CreateTrancodingStream(innerStream))
            {
                var writer = new StreamWriter(sequenceValidationTranscodingStream);
                writer.Write(TestText);
                writer.Flush();
                sequenceValidationTranscodingStream.Flush();

                innerStream.Seek(0, SeekOrigin.Begin);

                var hashBuffer = new byte[4];
                innerStream.Read(hashBuffer, 0, 4);
                var reader = new StreamReader(innerStream);
                var actualText = reader.ReadToEnd();

                CollectionAssert.AreEqual(hashBytes, hashBuffer);
                Assert.AreEqual(TestText, actualText);
            }
        }
    }
}
