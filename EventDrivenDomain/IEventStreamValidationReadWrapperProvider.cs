namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidationReadWrapperProvider
    {
        IEventStreamValidationReadWrapper GetValidationReadWrapper(Stream stream);
    }
}