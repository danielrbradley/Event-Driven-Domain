namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public class HashChecksumTranscodingStreamFactory : ITranscodingStreamFactory
    {
        private readonly IStreamHashGenerator streamHashGenerator;

        private readonly ITranscodingStreamFactory transcodingStreamFactory;

        public HashChecksumTranscodingStreamFactory(IStreamHashGenerator streamHashGenerator)
            : this(streamHashGenerator, new PassThroughTranscodingStreamFactory())
        {
        }

        public HashChecksumTranscodingStreamFactory(IStreamHashGenerator streamHashGenerator, ITranscodingStreamFactory transcodingStreamFactory)
        {
            this.streamHashGenerator = streamHashGenerator;
            this.transcodingStreamFactory = transcodingStreamFactory;
        }

        public Stream CreateTrancodingStream(Stream outputStream)
        {
            using (var innerTranscodingStream = this.transcodingStreamFactory.CreateTrancodingStream(outputStream))
            {
                return new HashChecksumTranscodingStream(innerTranscodingStream, this.streamHashGenerator);
            }
        }

        internal class HashChecksumTranscodingStream : Stream
        {
            private readonly Stream outputStream;

            private readonly IStreamHashGenerator streamHashGenerator;

            private readonly Stream internalStream;

            public HashChecksumTranscodingStream(Stream outputStream, IStreamHashGenerator streamHashGenerator)
            {
                this.outputStream = outputStream;
                this.streamHashGenerator = streamHashGenerator;
                this.internalStream = new MemoryStream();
            }

            public override void Flush()
            {
                this.internalStream.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return this.internalStream.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                this.internalStream.SetLength(value);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return this.internalStream.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                this.internalStream.Write(buffer, offset, count);
            }

            public override bool CanRead
            {
                get
                {
                    return this.internalStream.CanRead;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return this.internalStream.CanSeek;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return this.internalStream.CanWrite;
                }
            }

            public override long Length
            {
                get
                {
                    return this.internalStream.Length;
                }
            }

            public override long Position
            {
                get
                {
                    return this.internalStream.Position;
                }

                set
                {
                    this.internalStream.Position = value;
                }
            }

            protected override void Dispose(bool disposing)
            {
                this.internalStream.Seek(0, SeekOrigin.Begin);
                var hash = this.streamHashGenerator.GenerateHash(this.internalStream);
                this.internalStream.Seek(0, SeekOrigin.Begin);
                this.internalStream.CopyTo(this.outputStream);
                var buffer = hash.GetBytes();
                this.outputStream.Write(buffer, 0, buffer.Length);
                this.internalStream.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
