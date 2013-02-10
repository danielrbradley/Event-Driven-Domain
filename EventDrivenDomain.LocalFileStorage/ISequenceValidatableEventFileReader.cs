namespace EventDrivenDomain.LocalFileStorage
{
    public interface ISequenceValidatableEventFileReader<TBaseCommand>
    {
        SequenceValidatableEvent<TBaseCommand> Read(string filePath);
    }
}