﻿namespace EventDrivenDomain
{
    using System.IO;

    public interface IStreamEventWriter<TBaseCommand>
    {
        void Write(Stream stream, Event<TBaseCommand> eventToWrite);
    }
}