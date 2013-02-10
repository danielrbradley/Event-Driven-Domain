namespace EventDrivenDomain
{
    using System.IO;

    public class StreamEventWriter<TBaseCommand> : IStreamEventWriter<TBaseCommand>
    {
        private readonly IEventEncoder<TBaseCommand> eventEncoder;

        private readonly ITranscodingStreamFactory transcodingStreamFactory;

        public StreamEventWriter(IEventEncoder<TBaseCommand> eventEncoder)
        {
            this.eventEncoder = eventEncoder;
            this.transcodingStreamFactory = new PassThroughTranscodingStreamFactory();
        }

        public StreamEventWriter(IEventEncoder<TBaseCommand> eventEncoder, ITranscodingStreamFactory transcodingStreamFactory)
        {
            this.eventEncoder = eventEncoder;
            this.transcodingStreamFactory = transcodingStreamFactory;
        }

        public void Write(Stream stream, Event<TBaseCommand> eventToWrite)
        {
            using (var streamTrancodeAdapter = this.transcodingStreamFactory.CreateTrancodingStream(stream))
            {
                this.eventEncoder.WriteEvent(streamTrancodeAdapter, eventToWrite);
            }
        }
    }
}