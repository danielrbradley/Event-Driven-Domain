namespace EventDrivenDomain
{
    using System.IO;

    public interface IHashedStreamWriter
    {
        void Write(Stream inputStream, Stream outputStream, string previousHash);
    }
}