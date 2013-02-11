namespace EventDrivenDomain.EventStore
{
    using System;

    public sealed class Event<TBaseCommand>
    {
        private readonly DateTime timestamp;

        private readonly TBaseCommand commandExecuted;

        public Event(IMessage<TBaseCommand> commandExecuted, DateTime timestamp)
        {
            this.timestamp = timestamp;
            this.commandExecuted = commandExecuted.Command;
        }

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
        }

        public TBaseCommand CommandExecuted
        {
            get
            {
                return commandExecuted;
            }
        }
    }
}