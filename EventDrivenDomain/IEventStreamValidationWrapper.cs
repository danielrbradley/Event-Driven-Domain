namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidationWrapper
    {
        void Validate(string previousHash, out string hash);

        Stream InnerStream { get; }
    }
}