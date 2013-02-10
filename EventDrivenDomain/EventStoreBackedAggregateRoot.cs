namespace EventDrivenDomain
{
    using System;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class EventStoreBackedAggregateRoot<TBaseCommand, TAggregate> : IObservable<Event<TBaseCommand>>, IDisposable
        where TAggregate : class, IAggregate<TAggregate, TBaseCommand>
    {
        private readonly IWritableEventStore<TBaseCommand> eventStore;

        private readonly ConcurrentBlockingQueue<Message<TBaseCommand>> queue;

        private readonly Subject<Event<TBaseCommand>> observableSubject = new Subject<Event<TBaseCommand>>();

        private TAggregate state;

        private readonly CancellationTokenSource disposeTokenSource = new CancellationTokenSource();

        private readonly ManualResetEvent disposeCompleted = new ManualResetEvent(true);

        protected EventStoreBackedAggregateRoot(IWritableEventStore<TBaseCommand> eventStore, TAggregate initialState)
        {
            this.eventStore = eventStore;
            this.state = initialState;

            this.queue = new ConcurrentBlockingQueue<Message<TBaseCommand>>(disposeTokenSource.Token);

            var task = new Task(this.QueueProcessor, TaskCreationOptions.LongRunning);
            task.Start();
        }

        private void QueueProcessor()
        {
            disposeCompleted.Reset();

            while (!disposeTokenSource.IsCancellationRequested)
            {
                var message = queue.WaitDequeue();
                Event<TBaseCommand> newEvent;

                if (message == null)
                {
                    continue;
                }

                try
                {
                    message.Start();
                    var newState = this.State.Apply(message.Command);
                    newEvent = this.eventStore.Write(message);
                    Interlocked.Exchange(ref this.state, newState);
                }
                catch (Exception ex)
                {
                    message.Error(ex);
                    continue;
                }

                message.Complete();
                observableSubject.OnNext(newEvent);
            }

            disposeCompleted.Set();
        }

        protected virtual void Execute(TBaseCommand command)
        {
            var message = new Message<TBaseCommand>(command);
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

        public IDisposable Subscribe(IObserver<Event<TBaseCommand>> observer)
        {
            return this.observableSubject.Subscribe(observer);
        }

        public void Dispose()
        {
            disposeTokenSource.Cancel();
            foreach (var message in queue)
            {
                message.Dispose();
            }

            disposeCompleted.WaitOne();
            disposeTokenSource.Dispose();
        }
    }
}
