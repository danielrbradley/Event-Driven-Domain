namespace EventDrivenDomain.Tests.Fakes
{
    using System.IO;

    /// <summary>
    /// A test validating writer which skips any kind of hash validation and simply
    /// writes the inner stream to the outer stream.
    /// </summary>
    public class PassThroughEventStreamValidatingWriter : IHashedStreamWriter
    {
        public void Write(Stream inputStream, Stream outputStream, string previousHash)
        {
            inputStream.CopyTo(outputStream);
        }
    }
}