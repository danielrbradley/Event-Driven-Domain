namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface ITranscodingStreamFactory
    {
        Stream CreateTrancodingStream(Stream innerStream);
    }
}