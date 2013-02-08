namespace EventDrivenDomain.Tests.Fakes.Users
{
    using System;

    public class UserAggregate : IAggregate<UserAggregate, IUserAction>
    {
        public UserAggregate Apply(IUserAction action)
        {
            if (action.GetType() == typeof(UpdateName))
            {
                var actionAsUpdateName = (UpdateName)action;
                var clone = (UserAggregate)this.MemberwiseClone();
                clone.Name = actionAsUpdateName.UpdatedName;
                return clone;
            }

            throw new NotImplementedException();
        }

        public string Name { get; private set; }
    }
}