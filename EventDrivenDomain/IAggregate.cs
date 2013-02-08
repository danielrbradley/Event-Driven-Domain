namespace EventDrivenDomain
{
    public interface IAggregate<out TAggregate, in TBaseCommand>
    {
        TAggregate Apply(TBaseCommand action);
    }
}