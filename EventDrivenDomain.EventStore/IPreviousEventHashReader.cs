namespace EventDrivenDomain.EventStore
{
    public interface IPreviousEventHashReader
    {
        Hash ReadPreviousHash();
    }
}