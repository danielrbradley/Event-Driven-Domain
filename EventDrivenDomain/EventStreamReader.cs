namespace EventDrivenDomain
{
    using System.IO;

    public class EventStreamReader<TBaseCommand> : IEventStreamReader<TBaseCommand>
    {
        private readonly IEventDecoder<TBaseCommand> eventDecoder;

        private readonly IEventStreamValidationReadWrapperProvider eventStreamValidationReadWrapperProvider;

        public EventStreamReader(IEventDecoder<TBaseCommand> eventDecoder, IEventStreamValidationReadWrapperProvider eventStreamValidationReadWrapperProvider)
        {
            this.eventDecoder = eventDecoder;
            this.eventStreamValidationReadWrapperProvider = eventStreamValidationReadWrapperProvider;
        }

        public Event<TBaseCommand> Read(EventReadState state, Stream stream, out string hash)
        {
            var validationReadWrapper = eventStreamValidationReadWrapperProvider.GetValidationReadWrapper(stream);
            validationReadWrapper.Validate(state.PreviousHash, out hash);
            var eventToReturn = this.eventDecoder.ReadEvent(validationReadWrapper.InnerStream);
            return eventToReturn;
        }
    }
}