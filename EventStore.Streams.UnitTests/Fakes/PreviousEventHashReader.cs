namespace EventDrivenDomain.EventStore.Streams.UnitTests.Fakes
{
    public class PreviousEventHashReader : IPreviousEventHashReader
    {
        private readonly Hash hash;

        public PreviousEventHashReader(Hash hash)
        {
            this.hash = hash;
        }

        public Hash ReadPreviousHash()
        {
            return this.hash;
        }
    }
}