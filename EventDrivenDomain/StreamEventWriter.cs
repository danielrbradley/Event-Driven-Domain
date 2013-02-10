namespace EventDrivenDomain
{
    using System.IO;

    public class StreamEventWriter<TBaseCommand> : IStreamEventWriter<TBaseCommand>
    {
        private readonly IEventEncoder<TBaseCommand> eventEncoder;

        private readonly IStreamTranscodeAdapterFactory streamTranscodeAdapterFactory;

        public StreamEventWriter(IEventEncoder<TBaseCommand> eventEncoder)
        {
            this.eventEncoder = eventEncoder;
            this.streamTranscodeAdapterFactory = new PassThroughStreamTranscodeAdapterFactory();
        }

        public StreamEventWriter(IEventEncoder<TBaseCommand> eventEncoder, IStreamTranscodeAdapterFactory streamTranscodeAdapterFactory)
        {
            this.eventEncoder = eventEncoder;
            this.streamTranscodeAdapterFactory = streamTranscodeAdapterFactory;
        }

        public void Write(Stream stream, Event<TBaseCommand> eventToWrite)
        {
            using (var streamTrancodeAdapter = this.streamTranscodeAdapterFactory.CreateStreamTrancodeAdapter(stream))
            {
                this.eventEncoder.WriteEvent(streamTrancodeAdapter.InputStream, eventToWrite);
            }
        }
    }
}