namespace EventDrivenDomain
{
    using System;
    using System.Linq;

    public struct Hash
    {
        private byte[] hash;

        private bool isNone;

        public static readonly Hash None = new Hash();

        public Hash(byte[] hash)
            : this()
        {
            if (hash == null)
            {
                throw new ArgumentNullException("hash", "hash is null.");
            }

            this.hash = hash;
        }

        private byte[] HashOrDefault
        {
            get
            {
                if (hash == null)
                {
                    this.isNone = true;
                    this.hash = new byte[0];
                }

                return this.hash;
            }
        }

        public bool IsNone
        {
            get
            {
                if (hash == null)
                {
                    this.isNone = true;
                    this.hash = new byte[0];
                }

                return this.isNone;
            }
        }

        public byte[] GetBytes()
        {
            return (byte[])HashOrDefault.Clone();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return BitConverter.ToString(this.HashOrDefault);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:EventDrivenDomain.Hash"/> is equal to the current <see cref="T:EventDrivenDomain.Hash"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="other">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public bool Equals(Hash other)
        {
            if (this.IsNone || other.IsNone)
            {
                return false;
            }

            return other.HashOrDefault.SequenceEqual(this.HashOrDefault);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Array"/> is equal to the current <see cref="T:EventDrivenDomain.Hash"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="other">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public bool Equals(byte[] other)
        {
            if (other == null || this.IsNone)
            {
                return false;
            }

            return other.SequenceEqual(this.HashOrDefault);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Hash))
            {
                return false;
            }

            return this.Equals((Hash)obj);
        }

        public override int GetHashCode()
        {
            var hashOrDefault = this.HashOrDefault;
            if (hashOrDefault.Length < 4)
            {
                var padded = new byte[4];
                hashOrDefault.CopyTo(padded, 0);
                return BitConverter.ToInt32(padded, 0);
            }

            return BitConverter.ToInt32(hashOrDefault, 0);
        }

        public static bool operator ==(Hash a, Hash b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Hash a, Hash b)
        {
            return !(a == b);
        }
    }
}