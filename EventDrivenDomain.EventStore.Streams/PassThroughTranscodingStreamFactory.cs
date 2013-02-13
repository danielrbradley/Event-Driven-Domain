namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public class PassThroughTranscodingStreamFactory : ITranscodingStreamFactory
    {
        public Stream CreateTrancodingStream(Stream innerStream)
        {
            return new PassThroughStream(innerStream);
        }

        internal class PassThroughStream : Stream
        {
            private readonly Stream innerStream;

            public PassThroughStream(Stream innerStream)
            {
                this.innerStream = innerStream;
            }

            public override void Flush()
            {
                this.innerStream.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return this.innerStream.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                this.innerStream.SetLength(value);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return this.innerStream.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                this.innerStream.Write(buffer, offset, count);
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
                    return this.innerStream.CanWrite;
                }
            }

            public override long Length
            {
                get
                {
                    return this.innerStream.Length;
                }
            }

            public override long Position
            {
                get
                {
                    return this.innerStream.Position;
                }

                set
                {
                    this.innerStream.Position = value;
                }
            }
        }
    }
}