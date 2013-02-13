namespace EventDrivenDomain.EventStore.Streams
{
    using System;
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

        public Stream CreateTrancodingStream(Stream innerStream)
        {
            using (var innerTranscodingStream = this.transcodingStreamFactory.CreateTrancodingStream(innerStream))
            {
                return new HashChecksumTranscodingStream(innerTranscodingStream, this.streamHashGenerator);
            }
        }

        internal class HashChecksumTranscodingStream : Stream
        {
            private readonly Stream innerStream;

            private readonly IStreamHashGenerator streamHashGenerator;

            private readonly MemoryStream bufferStream;

            public HashChecksumTranscodingStream(Stream innerStream, IStreamHashGenerator streamHashGenerator)
            {
                if (!innerStream.CanWrite || !innerStream.CanRead || !innerStream.CanSeek)
                {
                    throw new ArgumentException("Inner stream must be seekable, readable and writable", "innerStream");
                }

                this.innerStream = innerStream;
                this.streamHashGenerator = streamHashGenerator;

                this.bufferStream = new MemoryStream();
                innerStream.Position = 0;
                innerStream.CopyTo(this.bufferStream);

                this.bufferStream.Position = innerStream.Position;
            }

            public override void Flush()
            {
                var bufferStartPosition = this.bufferStream.Position;
                this.bufferStream.Seek(0, SeekOrigin.Begin);

                var hash = this.streamHashGenerator.GenerateHash(this.bufferStream);
                var hashBuffer = hash.GetBytes();

                this.bufferStream.Seek(0, SeekOrigin.Begin);
                this.innerStream.Seek(0, SeekOrigin.Begin);
                this.bufferStream.CopyTo(this.innerStream);

                this.innerStream.Write(hashBuffer, 0, hashBuffer.Length);
                this.bufferStream.Position = bufferStartPosition;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return this.bufferStream.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                this.innerStream.SetLength(value + this.streamHashGenerator.GetHashSize());
                this.bufferStream.SetLength(value);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return this.bufferStream.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                this.bufferStream.Write(buffer, offset, count);
            }

            public override bool CanRead
            {
                get
                {
                    return this.innerStream.CanRead;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return this.innerStream.CanSeek;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return true;
                }
            }

            public override long Length
            {
                get
                {
                    return this.bufferStream.Length;
                }
            }

            public override long Position
            {
                get
                {
                    return this.bufferStream.Position;
                }

                set
                {
                    this.bufferStream.Position = value;
                }
            }
        }
    }
}
