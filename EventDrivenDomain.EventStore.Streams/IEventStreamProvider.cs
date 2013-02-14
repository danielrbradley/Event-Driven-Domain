namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IEventStreamProvider
    {
        Stream GetPreviousEventStream();
    }
}
