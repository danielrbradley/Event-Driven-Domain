namespace EventDrivenDomain
{
    using System;
    using System.IO;

    public interface IStreamTranscodeAdapter : IDisposable
    {
        Stream InputStream { get; }
    }
}