namespace EventDrivenDomain.Tests.Fakes
{
    using System.IO;

    /// <summary>
    /// A test validating writer which skips any kind of hash validation and simply
    /// writes the inner stream to the outer stream.
    /// </summary>
    public class PassThroughEventStreamValidatingWriter : IEventStreamValidatingWriter
    {
        public void Write(Stream stream, string previousHash, Stream innerStream)
        {
            innerStream.CopyTo(stream);
        }
    }
}