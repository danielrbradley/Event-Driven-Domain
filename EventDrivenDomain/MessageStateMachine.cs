namespace EventDrivenDomain
{
    using System;
    using System.Threading;

    public sealed class MessageStateMachine
    {
        private MessageState state;

        public MessageStateMachine()
            : this(MessageState.Created)
        {
        }

        public MessageStateMachine(MessageState initialState)
        {
            this.state = initialState;
        }

        public MessageState CurrentState
        {
            get
            {
                return this.state;
            }
        }

        public bool TryChangeState(MessageState newState, out MessageState oldState)
        {
            var currentState = this.state;

            while (true)
            {
                if (!currentState.CanTransitionTo(newState))
                {
                    oldState = currentState;
                    return false;
                }

                Interlocked.CompareExchange(ref this.state, newState, currentState);

                if (this.state == newState)
                {
                    oldState = currentState;
                    return true;
                }
            }
        }

        public void ChangeState(MessageState newState)
        {
            MessageState currentState;
            if (!this.TryChangeState(newState, out currentState))
            {
                throw new InvalidOperationException(
                    string.Format("Invalid state change from {0} to {1}", currentState.Name, newState.Name));
            }
        }
    }
}