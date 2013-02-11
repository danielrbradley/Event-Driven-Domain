namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface ISerializer<in T>
    {
        void SerializeToStream(Stream destination, T objectToWrite);
    }
}
