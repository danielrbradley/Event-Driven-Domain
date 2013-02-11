namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IHashedStreamReader
    {
        /// <summary>
        /// Validate a stream containing a hashed, serialized event.
        /// </summary>
        /// <param name="inputStream">Stream containing the hashed serialized event.</param>
        /// <param name="outputStream">Stream to write the checked content to.</param>
        /// <param name="previousHash">Returns the previous hash read from the stream.</param>
        /// <param name="expectedStreamHash">Returns the expected hash of the input stream (excluding expected hash).</param>
        /// <param name="actualStreamHash">Returns the actual hash of the input stream (excluding expected hash).</param>
        void Read(Stream inputStream, Stream outputStream, out Hash previousHash, out Hash expectedStreamHash, out Hash actualStreamHash);
    }
}