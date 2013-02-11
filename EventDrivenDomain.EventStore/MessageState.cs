namespace EventDrivenDomain.EventStore
{
    using System.Collections.Generic;

    public sealed class MessageState
    {
        private readonly string name;

        private readonly HashSet<string> allowedTransitions;

        private MessageState(string name, params string[] allowedTransitions)
        {
            this.name = name;
            this.allowedTransitions = new HashSet<string>(allowedTransitions);
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool CanTransitionTo(MessageState newState)
        {
            return allowedTransitions.Contains(newState.Name);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.Name;
        }

        public static readonly MessageState Created = new MessageState("Created", "Started", "Cancelled");

        public static readonly MessageState Started = new MessageState("Started", "Completed", "Cancelled");

        public static readonly MessageState Completed = new MessageState("Completed");

        public static readonly MessageState Cancelled = new MessageState("Cancelled");
    }
}