﻿namespace EventDrivenDomain.Tests.Fakes.Users
{
    public class User : EventStoreBackedAggregateRoot<IUserCommand, UserAggregate>
    {
        public User(IEventWriter<IUserCommand> eventStore, UserAggregate initialState)
            : base(eventStore, initialState)
        {
        }

        public void ChangeName(string updatedName)
        {
            this.Execute(new UpdateName(updatedName));
        }
    }
}
