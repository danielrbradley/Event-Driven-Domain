namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidationWriteWrapper
    {
        void Write(Stream stream, string previousHash, Stream innerStream);
    }
}