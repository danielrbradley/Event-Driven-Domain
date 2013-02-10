namespace EventDrivenDomain
{
    using System.IO;

    public interface ISequenceValidatableEventStreamReader<TBaseCommand>
    {
        SequenceValidatableEvent<TBaseCommand> Read(Stream stream);
    }
}