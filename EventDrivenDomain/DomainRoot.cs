namespace EventDrivenDomain
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DomainRoot<TBaseAction, TAggregate> : IObservable<Event<TBaseAction>>, IDisposable
        where TAggregate : class, IAggregate<TAggregate, TBaseAction>
    {
        private readonly IEventStore<TBaseAction> eventStore;

        private readonly ConcurrentBlockingQueue<Message<TBaseAction>> queue;

        private readonly Subject<Event<TBaseAction>> observableSubject = new Subject<Event<TBaseAction>>();

        private TAggregate state;

        private readonly CancellationTokenSource disposeTokenSource = new CancellationTokenSource();

        private readonly ManualResetEvent disposeCompleted = new ManualResetEvent(true);

        protected DomainRoot(IEventStore<TBaseAction> eventStore, TAggregate initialState)
        {
            this.eventStore = eventStore;
            this.state = initialState;

            this.queue = new ConcurrentBlockingQueue<Message<TBaseAction>>(disposeTokenSource.Token);

            var task = new Task(this.QueueProcessor, TaskCreationOptions.LongRunning);
            task.Start();
        }

        private void QueueProcessor()
        {
            disposeCompleted.Reset();

            while (!disposeTokenSource.IsCancellationRequested)
            {
                var message = queue.WaitDequeue();
                Event<TBaseAction> actionEvent;

                if (message == null)
                {
                    continue;
                }

                try
                {
                    var newState = this.State.Apply(message.Action);
                    actionEvent = this.eventStore.Write(message.Action);
                    Interlocked.Exchange(ref this.state, newState);
                }
                catch (Exception ex)
                {
                    message.Error(ex);
                    continue;
                }

                message.Complete();
                observableSubject.OnNext(actionEvent);
            }

            disposeCompleted.Set();
        }

        protected virtual void Update(TBaseAction action)
        {
            var message = new Message<TBaseAction>(action);
            this.queue.Enqueue(message);
            message.WaitCompletion();
        }

        public TAggregate State
        {
            get
            {
                return this.state;
            }
        }

        public IDisposable Subscribe(IObserver<Event<TBaseAction>> observer)
        {
            return this.observableSubject.Subscribe(observer);
        }

        public void Dispose()
        {
            disposeTokenSource.Cancel();
            disposeCompleted.WaitOne();
            disposeTokenSource.Dispose();
        }
    }
}
