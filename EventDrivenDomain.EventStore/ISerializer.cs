namespace EventDrivenDomain.EventStore
{
    using System.IO;

    public interface ISerializer<in T>
    {
        void SerializeToStream(Stream stream, T eventToWrite);
    }
}
