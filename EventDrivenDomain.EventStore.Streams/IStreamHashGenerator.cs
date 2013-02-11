namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IStreamHashGenerator
    {
        Hash GenerateHash(Stream stream);
    }
}