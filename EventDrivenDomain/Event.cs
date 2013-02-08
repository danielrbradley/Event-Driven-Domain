namespace EventDrivenDomain
{
    using System;

    public sealed class Event<TBaseCommand>
    {
        private readonly DateTime timestamp;

        private readonly TBaseCommand commandExecuted;

        private readonly Guid id;

        public Event(Message<TBaseCommand> commandExecuted, DateTime timestamp)
        {
            this.id = commandExecuted.Id;
            this.timestamp = timestamp;
            this.commandExecuted = commandExecuted.Command;
        }

        public Guid Id
        {
            get
            {
                return id;
            }
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