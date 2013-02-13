namespace EventDrivenDomain.EventStore.Streams
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class CryptoStreamHashGenerator : IStreamHashGenerator
    {
        private readonly HashAlgorithm hashAlgorithm;

        public CryptoStreamHashGenerator(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }

        public Hash GenerateHash(Stream stream)
        {
            var hashBytes = this.hashAlgorithm.ComputeHash(stream);
            var hash = new Hash(hashBytes);
            return hash;
        }

        public int GetHashSize()
        {
            return (int)Math.Ceiling((double)this.hashAlgorithm.HashSize / 8);
        }
    }
}
