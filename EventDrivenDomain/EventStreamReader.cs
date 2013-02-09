namespace EventDrivenDomain
{
    using System.IO;

    public class EventStreamReader<TBaseCommand> : IEventStreamReader<TBaseCommand>
    {
        private readonly IEventDecoder<TBaseCommand> eventDecoder;

        private readonly IEventStreamValidationWrapperProvider eventStreamWrapperProvider;

        public EventStreamReader(IEventDecoder<TBaseCommand> eventDecoder, IEventStreamValidationWrapperProvider eventStreamValidationWrapperProvider)
        {
            this.eventDecoder = eventDecoder;
            this.eventStreamWrapperProvider = eventStreamValidationWrapperProvider;
        }

        public Event<TBaseCommand> Read(EventReadState state, Stream stream, out string hash)
        {
            var validationWrapper = eventStreamWrapperProvider.GetValidationWrapper(stream);
            validationWrapper.Validate(state.PreviousHash, out hash);
            var eventToReturn = this.eventDecoder.ReadEvent(validationWrapper.InnerStream);
            return eventToReturn;
        }
    }
}