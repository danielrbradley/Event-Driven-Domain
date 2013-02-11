namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    public interface ISequenceValidatableEventFileReader<TBaseCommand>
    {
        SequenceValidatableEvent<TBaseCommand> Read(string filePath);
    }
}