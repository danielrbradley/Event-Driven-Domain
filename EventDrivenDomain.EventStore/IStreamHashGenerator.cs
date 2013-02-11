namespace EventDrivenDomain.EventStore
{
    using System.IO;

    public interface IStreamHashGenerator
    {
        Hash GenerateHash(Stream stream);
    }
}