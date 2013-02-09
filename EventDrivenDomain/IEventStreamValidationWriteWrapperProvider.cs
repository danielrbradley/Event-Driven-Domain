namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidationWriteWrapperProvider
    {
        IEventStreamValidationWriteWrapper GetValidationWriteWrapper();
    }
}