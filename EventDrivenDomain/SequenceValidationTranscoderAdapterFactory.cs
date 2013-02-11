namespace EventDrivenDomain
{
    using System;
    using System.IO;

    public class SequenceValidationTranscoderAdapterFactory : ITranscodingStreamFactory
    {
        private readonly IPreviousEventHashReader previousEventHashReader;

        private readonly ITranscodingStreamFactory transcodingStreamFactory;

        public SequenceValidationTranscoderAdapterFactory(IPreviousEventHashReader previousEventHashReader)
            : this(previousEventHashReader, new PassThroughTranscodingStreamFactory())
        {
        }

        public SequenceValidationTranscoderAdapterFactory(IPreviousEventHashReader previousEventHashReader, ITranscodingStreamFactory transcodingStreamFactory)
        {
            this.previousEventHashReader = previousEventHashReader;
            this.transcodingStreamFactory = transcodingStreamFactory;
        }

        public Stream CreateTrancodingStream(Stream outputStream)
        {
            using (var transcodingStream = this.transcodingStreamFactory.CreateTrancodingStream(outputStream))
            {
                return new SequenceValidationTranscoderAdapterStream(transcodingStream, previousEventHashReader);
            }
        }

        internal class SequenceValidationTranscoderAdapterStream : Stream
        {
            private readonly Stream outputStream;

            private readonly int hashByteCount;

            public SequenceValidationTranscoderAdapterStream(Stream outputStream, IPreviousEventHashReader previousEventHashReader)
            {
                var previousHash = previousEventHashReader.ReadPreviousHash();
                var buffer = previousHash.GetBytes();
                this.hashByteCount = buffer.Length;
                outputStream.Write(buffer, 0, this.hashByteCount);
                this.outputStream = outputStream;
            }

            public override void Flush()
            {
                this.outputStream.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                // TODO: Add validation to stop seeking back into hash.
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        return this.outputStream.Seek(offset + this.hashByteCount, origin) - this.hashByteCount;

                    case SeekOrigin.Current:
                        if (this.Position - offset < 0)
                        {
                            throw new ArgumentOutOfRangeException(
                                "offset", "Cannot seek to before the start of the stream.");
                        }

                        return this.outputStream.Seek(offset, origin);

                    case SeekOrigin.End:
                        if (this.Length - offset < 0)
                        {
                            throw new ArgumentOutOfRangeException(
                                "offset", "Cannot seek to before the start of the stream.");
                        }

                        return this.outputStream.Seek(offset, origin);

                    default:
                        throw new ArgumentOutOfRangeException("origin", "Invalid seek orgin");
                }
            }

            public override void SetLength(long value)
            {
                this.outputStream.SetLength(value + this.hashByteCount);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return this.outputStream.Read(buffer, offset + this.hashByteCount, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                this.outputStream.Write(buffer, offset + this.hashByteCount, count);
            }

            public override bool CanRead
            {
                get
                {
                    return this.outputStream.CanRead;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return this.outputStream.CanSeek;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return this.outputStream.CanWrite;
                }
            }

            public override long Length
            {
                get
                {
                    return this.outputStream.Length - this.hashByteCount;
                }
            }

            public override long Position
            {
                get
                {
                    return this.outputStream.Position - this.hashByteCount;
                }

                set
                {
                    this.outputStream.Position = value + this.hashByteCount;
                }
            }
        }
    }
}