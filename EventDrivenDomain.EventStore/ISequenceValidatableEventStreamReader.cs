namespace EventDrivenDomain.EventStore
{
    using System.IO;

    public interface ISequenceValidatableEventStreamReader<TBaseCommand>
    {
        SequenceValidatableEvent<TBaseCommand> Read(Stream stream);
    }
}