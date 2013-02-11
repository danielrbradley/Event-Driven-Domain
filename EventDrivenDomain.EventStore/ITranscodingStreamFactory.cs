namespace EventDrivenDomain.EventStore
{
    using System.IO;

    public interface ITranscodingStreamFactory
    {
        Stream CreateTrancodingStream(Stream outputStream);
    }
}