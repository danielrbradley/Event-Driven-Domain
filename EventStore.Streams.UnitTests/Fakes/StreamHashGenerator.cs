namespace EventDrivenDomain.EventStore.Streams.UnitTests.Fakes
{
    using System.IO;

    public class StreamHashGenerator : IStreamHashGenerator
    {
        private readonly Hash hash;

        private readonly int length;

        public StreamHashGenerator(Hash hash)
        {
            this.hash = hash;
            this.length = hash.GetBytes().Length;
        }

        public Hash GenerateHash(Stream stream)
        {
            return hash;
        }

        public int GetHashSize()
        {
            return this.length;
        }
    }
}