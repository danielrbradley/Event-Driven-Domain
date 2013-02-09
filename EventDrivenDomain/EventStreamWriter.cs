namespace EventDrivenDomain
{
    using System.IO;

    public class EventStreamWriter<TBaseCommand> : IEventStreamWriter<TBaseCommand>
    {
        private readonly IPreviousEventHashReader previousEventHashReader;

        private readonly IEventEncoder<TBaseCommand> eventEncoder;

        private readonly IHashedStreamWriter eventStreamValidatingWriter;

        public EventStreamWriter(IPreviousEventHashReader previousEventHashReader, IEventEncoder<TBaseCommand> eventEncoder, IHashedStreamWriter eventStreamValidatingWriter)
        {
            this.previousEventHashReader = previousEventHashReader;
            this.eventEncoder = eventEncoder;
            this.eventStreamValidatingWriter = eventStreamValidatingWriter;
        }

        public void Write(Stream stream, Event<TBaseCommand> eventToWrite)
        {
            using (var eventStream = new MemoryStream())
            {
                this.eventEncoder.WriteEvent(eventStream, eventToWrite);
                eventStream.Seek(0, SeekOrigin.Begin);

                string previousHash = this.previousEventHashReader.ReadPreviousHash();

                eventStreamValidatingWriter.Write(eventStream, stream, previousHash);
            }
        }
    }
}