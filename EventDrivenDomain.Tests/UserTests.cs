namespace EventDrivenDomain.Tests
{
    using System.Linq;

    using EventDrivenDomain.Tests.Fakes;
    using EventDrivenDomain.Tests.Fakes.Users;

    using NUnit.Framework;

    class UserTests
    {
        [Test]
        public void Given_a_user_When_updating_the_name_Then_update_name_event_is_witten_to_the_store()
        {
            var eventStore = new ListEventStore<IUserAction>();
            var userAggregate = new UserAggregate();
            using (var user = new User(eventStore, userAggregate))
            {
                user.ChangeName("The Name");

                Assert.IsTrue(eventStore.Events.Any());
            }
        }
    }
}
