namespace EventDrivenDomain.EventStore.Streams
{
    using System;
    using System.IO;

    public class SequenceValidationTranscodingStreamFactory : ITranscodingStreamFactory
    {
        private readonly IPreviousEventHashReader previousEventHashReader;

        private readonly ITranscodingStreamFactory transcodingStreamFactory;

        public SequenceValidationTranscodingStreamFactory(IPreviousEventHashReader previousEventHashReader)
            : this(previousEventHashReader, new PassThroughTranscodingStreamFactory())
        {
        }

        public SequenceValidationTranscodingStreamFactory(IPreviousEventHashReader previousEventHashReader, ITranscodingStreamFactory transcodingStreamFactory)
        {
            this.previousEventHashReader = previousEventHashReader;
            this.transcodingStreamFactory = transcodingStreamFactory;
        }

        public Stream CreateTrancodingStream(Stream innerStream)
        {
            using (var transcodingStream = this.transcodingStreamFactory.CreateTrancodingStream(innerStream))
            {
                return new SequenceValidationTranscodingStream(transcodingStream, previousEventHashReader);
            }
        }

        internal class SequenceValidationTranscodingStream : Stream
        {
            private readonly Stream innerStream;

            private readonly int hashByteCount;

            public SequenceValidationTranscodingStream(Stream innerStream, IPreviousEventHashReader previousEventHashReader)
            {
                var previousHash = previousEventHashReader.ReadPreviousHash();
                var buffer = previousHash.GetBytes();
                this.hashByteCount = buffer.Length;
                innerStream.Write(buffer, 0, this.hashByteCount);
                this.innerStream = innerStream;
            }

            public override void Flush()
            {
                this.innerStream.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        return this.innerStream.Seek(offset + this.hashByteCount, origin) - this.hashByteCount;

                    case SeekOrigin.Current:
                        if (this.Position - offset < 0)
                        {
                            throw new ArgumentOutOfRangeException(
                                "offset", "Cannot seek to before the start of the stream.");
                        }

                        return this.innerStream.Seek(offset, origin);

                    case SeekOrigin.End:
                        if (this.Length - offset < 0)
                        {
                            throw new ArgumentOutOfRangeException(
                                "offset", "Cannot seek to before the start of the stream.");
                        }

                        return this.innerStream.Seek(offset, origin);

                    default:
                        throw new ArgumentOutOfRangeException("origin", "Invalid seek orgin");
                }
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
                    return this.innerStream.Length - this.hashByteCount;
                }
            }

            public override long Position
            {
                get
                {
                    return this.innerStream.Position - this.hashByteCount;
                }

                set
                {
                    this.innerStream.Position = value + this.hashByteCount;
                }
            }
        }
    }
}