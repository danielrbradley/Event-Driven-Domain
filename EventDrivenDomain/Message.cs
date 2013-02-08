namespace EventDrivenDomain
{
    using System;
    using System.Threading;

    public sealed class Message<T>
    {
        private readonly T action;

        private readonly ManualResetEvent resetEvent = new ManualResetEvent(false);

        private Exception error;

        public Message(T action)
        {
            this.action = action;
        }

        public T Action
        {
            get
            {
                return action;
            }
        }

        public void WaitCompletion()
        {
            resetEvent.WaitOne();

            if (error != null)
            {
                throw error;
            }
        }

        public void Complete()
        {
            resetEvent.Set();
        }

        public void Error(Exception ex)
        {
            this.error = ex;
            resetEvent.Set();
        }
    }
}
