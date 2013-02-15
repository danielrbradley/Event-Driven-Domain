namespace EventDrivenDomain.EventStore.IntegrationTests.Users
{
    public class User : EventStoreBackedAggregateRoot<IUserCommand, UserAggregate>
    {
        public User(IWritableEventStore<IUserCommand> eventStore, UserAggregate initialState)
            : base(eventStore, initialState)
        {
        }

        public void ChangeName(string updatedName)
        {
            this.Execute(new UpdateName(updatedName));
        }
    }
}
