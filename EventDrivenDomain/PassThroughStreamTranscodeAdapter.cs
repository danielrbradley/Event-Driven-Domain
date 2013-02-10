namespace EventDrivenDomain
{
    using System.IO;

    public class PassThroughStreamTranscodeAdapter : IStreamTranscodeAdapter
    {
        private readonly Stream stream;

        public PassThroughStreamTranscodeAdapter(Stream outputStream)
        {
            this.stream = outputStream;
        }

        public void Dispose()
        {
        }

        public Stream InputStream
        {
            get
            {
                return this.stream;
            }
        }
    }
}