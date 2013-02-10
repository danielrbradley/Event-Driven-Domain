namespace EventDrivenDomain
{
    using System.IO;

    public interface IStreamTranscodeAdapterFactory
    {
        IStreamTranscodeAdapter CreateStreamTrancodeAdapter(Stream outputStream);
    }
}