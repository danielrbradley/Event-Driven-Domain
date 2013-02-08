namespace EventDrivenDomain
{
    public interface IEventStore<TBaseAction>
    {
        Event<TBaseAction> Write(Message<TBaseAction> message);
    }
}