namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public class PassThroughTranscodingStreamFactory : ITranscodingStreamFactory
    {
        public Stream CreateTrancodingStream(Stream innerStream)
        {
            return new PassThroughStream(innerStream);
        }

        public class PassThroughStream : MemoryStream
        {
            private readonly Stream outputStream;

            public PassThroughStream(Stream outputStream)
            {
                this.outputStream = outputStream;
            }

            protected override void Dispose(bool disposing)
            {
                this.CopyTo(this.outputStream);
                base.Dispose(disposing);
            }
        }
    }
}