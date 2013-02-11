namespace EventDrivenDomain.EventStore.Tests
{
    using NUnit.Framework;

    public class MessageStateTransitionTests
    {
        [Test]
        public void Given_a_message_state_of_created_When_attempting_transition_to_started_Then_apply_transition()
        {
            var messageStateMachine = new MessageStateMachine();

            MessageState oldState;
            var isApplied = messageStateMachine.TryChangeState(MessageState.Started, out oldState);

            Assert.IsTrue(isApplied);
        }

        [Test]
        public void Given_a_message_state_of_created_When_attempting_transition_to_same_state_Then_reject_transition()
        {
            var messageStateMachine = new MessageStateMachine();

            MessageState oldState;
            var isApplied = messageStateMachine.TryChangeState(MessageState.Created, out oldState);

            Assert.IsFalse(isApplied);
        }
    }
}
