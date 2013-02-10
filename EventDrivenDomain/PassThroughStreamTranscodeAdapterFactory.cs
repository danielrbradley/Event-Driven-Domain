namespace EventDrivenDomain
{
    using System.IO;

    public class PassThroughStreamTranscodeAdapterFactory : IStreamTranscodeAdapterFactory
    {
        public IStreamTranscodeAdapter CreateStreamTrancodeAdapter(Stream outputStream)
        {
            return new PassThroughStreamTranscodeAdapter(outputStream);
        }
    }
}