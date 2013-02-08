namespace EventDrivenDomain
{
    using System;
    using System.Threading;

    public sealed class Message<TBaseCommand> : IDisposable
    {
        private readonly TBaseCommand command;

        private readonly ManualResetEvent resetEvent = new ManualResetEvent(false);

        private Exception error;

        private readonly MessageStateMachine messageState = new MessageStateMachine();

        private readonly Guid id;

        public Message(TBaseCommand command)
        {
            this.id = Guid.NewGuid();
            this.command = command;
        }

        public Guid Id
        {
            get
            {
                return id;
            }
        }

        public TBaseCommand Command
        {
            get
            {
                return command;
            }
        }

        public void WaitCompletion()
        {
            resetEvent.WaitOne();

            if (messageState.CurrentState == MessageState.Cancelled)
            {
                throw new ObjectDisposedException("message", "Message was disposed without being completed.");
            }

            if (error != null)
            {
                throw error;
            }
        }

        public MessageState State
        {
            get
            {
                return messageState.CurrentState;
            }
        }

        public void Start()
        {
            messageState.ChangeState(MessageState.Started);
        }

        public void Complete()
        {
            messageState.ChangeState(MessageState.Completed);
            resetEvent.Set();
        }

        public void Error(Exception ex)
        {
            MessageState oldState;
            if (messageState.TryChangeState(MessageState.Completed, out oldState))
            {
                this.error = ex;
                resetEvent.Set();
            }
        }

        public void Dispose()
        {
            messageState.ChangeState(MessageState.Cancelled);
            resetEvent.Set();
        }
    }
}
