namespace EventDrivenDomain
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading;

    public abstract class DomainRoot<TBaseAction, TAggregate> : IObservable<Event<TBaseAction>>
        where TAggregate : class, IAggregate<TAggregate, TBaseAction>
    {
        private readonly IEventStore<TBaseAction> eventStore;

        private readonly Subject<Event<TBaseAction>> observableSubject = new Subject<Event<TBaseAction>>();

        private readonly Subject<Message<TBaseAction>> messageQueue = new Subject<Message<TBaseAction>>();

        private TAggregate state;

        private readonly object queueProcessorLock = new object();

        protected DomainRoot(IEventStore<TBaseAction> eventStore, TAggregate initialState)
        {
            this.eventStore = eventStore;
            this.state = initialState;

            this.messageQueue.Delay(TimeSpan.Zero).Subscribe(
                message =>
                    {
                        // Make the message queue single threaded
                        lock (queueProcessorLock)
                        {
                            Event<TBaseAction> actionEvent;
                            try
                            {
                                var newState = this.State.Apply(message.Action);
                                actionEvent = this.eventStore.Write(message.Action);
                                Interlocked.Exchange(ref this.state, newState);
                            }
                            catch (Exception ex)
                            {
                                message.Error(ex);
                                return;
                            }

                            message.Complete();
                            observableSubject.OnNext(actionEvent);
                        }
                    });
        }

        protected virtual void Update(TBaseAction action)
        {
            var message = new Message<TBaseAction>(action);
            this.messageQueue.OnNext(message);
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
    }
}
