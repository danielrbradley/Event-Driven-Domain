namespace EventDrivenDomain
{
    using System;

    public class EventReadState
    {
        private string previousHash;

        public bool IsFirst
        {
            get
            {
                return this.previousHash == null;
            }
        }

        public string PreviousHash
        {
            get
            {
                return this.previousHash;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.previousHash = value;
            }
        }
    }
}