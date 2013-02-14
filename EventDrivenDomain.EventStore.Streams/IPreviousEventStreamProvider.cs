namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IPreviousEventStreamProvider
    {
        Stream GetPreviousEventStream();
    }
}
