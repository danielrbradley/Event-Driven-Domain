namespace EventDrivenDomain.LocalFileStorage
{
    public class EventReader<TBaseCommand> : IEventReader<TBaseCommand>, ILatestEventReader<TBaseCommand>
    {
        public IEventEnumerable<TBaseCommand> Events
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public Event<TBaseCommand> PreviousEvent
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}