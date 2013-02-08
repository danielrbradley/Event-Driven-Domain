namespace EventDrivenDomain.Tests.Fakes.Users
{
    public class UpdateName : IUserCommand
    {
        public string UpdatedName { get; private set; }

        public UpdateName(string updatedName)
        {
            this.UpdatedName = updatedName;
        }
    }
}