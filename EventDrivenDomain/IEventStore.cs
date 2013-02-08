namespace EventDrivenDomain
{
    public interface IEventStore<TBaseAction>
    {
        Event<TBaseAction> Write(TBaseAction action);
    }
}