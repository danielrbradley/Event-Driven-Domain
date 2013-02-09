namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventEncoder<TBaseCommand>
    {
        void WriteEvent(Stream stream, Event<TBaseCommand> eventToWrite);
    }
}