namespace EventDrivenDomain.EventStore.Streams
{
    using System.IO;

    public interface IStreamHashGenerator
    {
        Hash GenerateHash(Stream stream);

        /// <summary>
        /// Get the number of bytes in a generated hash.
        /// </summary>
        /// <returns>The number of bytes in a generated hash</returns>
        int GetHashSize();
    }
}