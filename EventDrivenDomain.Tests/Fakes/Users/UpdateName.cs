namespace EventDrivenDomain.Tests.Fakes.Users
{
    public class UpdateName : IUserAction
    {
        public string UpdatedName { get; private set; }

        public UpdateName(string updatedName)
        {
            this.UpdatedName = updatedName;
        }
    }
}