namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidationWrapperProvider
    {
        IEventStreamValidationWrapper GetValidationWrapper(Stream stream);
    }
}