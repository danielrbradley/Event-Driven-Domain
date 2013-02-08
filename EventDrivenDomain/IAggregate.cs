namespace EventDrivenDomain
{
    public interface IAggregate<out TAggregate, in TBaseAction>
    {
        TAggregate Apply(TBaseAction action);
    }
}