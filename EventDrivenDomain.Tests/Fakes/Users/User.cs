namespace EventDrivenDomain.Tests.Fakes.Users
{
    public class User : DomainRoot<IUserAction, UserAggregate>
    {
        public User(IEventStore<IUserAction> eventStore, UserAggregate initialState)
            : base(eventStore, initialState)
        {
        }

        public void ChangeName(string updatedName)
        {
            this.Update(new UpdateName(updatedName));
        }
    }
}
