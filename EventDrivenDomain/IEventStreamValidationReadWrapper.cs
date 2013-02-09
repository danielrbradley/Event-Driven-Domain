namespace EventDrivenDomain
{
    using System.IO;

    public interface IEventStreamValidationReadWrapper
    {
        void Validate(string previousHash, out string hash);

        Stream InnerStream { get; }
    }
}