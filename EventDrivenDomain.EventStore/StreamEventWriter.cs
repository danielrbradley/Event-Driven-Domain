namespace EventDrivenDomain.EventStore
{
    using System.IO;

    public class StreamEventWriter<TBaseCommand> : IStreamEventWriter<TBaseCommand>
    {
        private readonly ISerializer<Event<TBaseCommand>> serializer;

        private readonly ITranscodingStreamFactory transcodingStreamFactory;

        public StreamEventWriter(ISerializer<Event<TBaseCommand>> serializer)
        {
            this.serializer = serializer;
            this.transcodingStreamFactory = new PassThroughTranscodingStreamFactory();
        }

        public StreamEventWriter(ISerializer<Event<TBaseCommand>> serializer, ITranscodingStreamFactory transcodingStreamFactory)
        {
            this.serializer = serializer;
            this.transcodingStreamFactory = transcodingStreamFactory;
        }

        public void Write(Stream stream, Event<TBaseCommand> eventToWrite)
        {
            using (var streamTrancodeAdapter = this.transcodingStreamFactory.CreateTrancodingStream(stream))
            {
                this.serializer.SerializeToStream(streamTrancodeAdapter, eventToWrite);
            }
        }
    }
}
