namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidatingWriter
    {
        void Write(Stream stream, string previousHash, Stream innerStream);
    }
}