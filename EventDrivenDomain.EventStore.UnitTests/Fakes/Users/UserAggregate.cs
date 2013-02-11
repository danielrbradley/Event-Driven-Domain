namespace EventDrivenDomain.EventStore.Tests.Fakes.Users
{
    using System;

    public class UserAggregate : IAggregate<UserAggregate, IUserCommand>
    {
        public UserAggregate Apply(IUserCommand command)
        {
            if (command.GetType() == typeof(UpdateName))
            {
                var actionAsUpdateName = (UpdateName)command;
                var clone = (UserAggregate)this.MemberwiseClone();
                clone.Name = actionAsUpdateName.UpdatedName;
                return clone;
            }

            throw new NotImplementedException();
        }

        public string Name { get; private set; }
    }
}