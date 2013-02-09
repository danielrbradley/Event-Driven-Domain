namespace EventDrivenDomain
{
    using System.IO;

    public class EventStreamWriter<TBaseCommand> : IEventStreamWriter<TBaseCommand>
    {
        private readonly IPreviousEventHashReader previousEventHashReader;

        private readonly IEventEncoder<TBaseCommand> eventEncoder;

        private readonly IEventStreamValidatingWriter eventStreamValidatingWriter;

        public EventStreamWriter(IPreviousEventHashReader previousEventHashReader, IEventEncoder<TBaseCommand> eventEncoder, IEventStreamValidatingWriter eventStreamValidatingWriter)
        {
            this.previousEventHashReader = previousEventHashReader;
            this.eventEncoder = eventEncoder;
            this.eventStreamValidatingWriter = eventStreamValidatingWriter;
        }

        public void Write(Stream stream, Event<TBaseCommand> eventToWrite)
        {
            using (var innerStream = new MemoryStream())
            {
                this.eventEncoder.WriteEvent(innerStream, eventToWrite);
                innerStream.Seek(0, SeekOrigin.Begin);

                string previousHash = this.previousEventHashReader.ReadPreviousHash();

                eventStreamValidatingWriter.Write(stream, previousHash, innerStream);
            }
        }
    }
}