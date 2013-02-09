namespace EventDrivenDomain
{
    using System.IO;

    public class EventStreamWriter<TBaseCommand> : IEventStreamWriter<TBaseCommand>
    {
        private readonly IPreviousEventHashReader previousEventHashReader;

        private readonly IEventEncoder<TBaseCommand> eventEncoder;

        private readonly IEventStreamValidationWriteWrapperProvider eventStreamValidationWriteWrapperProvider;

        public EventStreamWriter(IPreviousEventHashReader previousEventHashReader, IEventEncoder<TBaseCommand> eventEncoder, IEventStreamValidationWriteWrapperProvider eventStreamValidationWriteWrapperProvider)
        {
            this.previousEventHashReader = previousEventHashReader;
            this.eventEncoder = eventEncoder;
            this.eventStreamValidationWriteWrapperProvider = eventStreamValidationWriteWrapperProvider;
        }

        public void Write(Stream stream, Event<TBaseCommand> eventToWrite)
        {
            using (var innerStream = new MemoryStream())
            {
                this.eventEncoder.WriteEvent(innerStream, eventToWrite);
                innerStream.Seek(0, SeekOrigin.Begin);

                string previousHash = this.previousEventHashReader.ReadPreviousHash();

                var eventStreamValidationWriteWrapper =
                    this.eventStreamValidationWriteWrapperProvider.GetValidationWriteWrapper();
                eventStreamValidationWriteWrapper.Write(stream, previousHash, innerStream);
            }
        }
    }
}