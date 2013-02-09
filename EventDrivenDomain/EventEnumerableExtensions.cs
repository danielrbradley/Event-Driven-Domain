namespace EventDrivenDomain
{
    using System.Collections;
    using System.Collections.Generic;

    public static class EventEnumerableExtensions
    {
        public static IEventEnumerable<T> AsEventEnumerable<T>(this IEnumerable<Event<T>> enumerable)
        {
            return new EventEnumerable<T>(enumerable);
        }

        private class EventEnumerable<T> : IEventEnumerable<T>
        {
            private readonly IEnumerable<Event<T>> enumerable;

            public EventEnumerable(IEnumerable<Event<T>> enumerable)
            {
                this.enumerable = enumerable;
            }

            public IEnumerator<Event<T>> GetEnumerator()
            {
                return this.enumerable.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.enumerable.GetEnumerator();
            }
        }
    }
}
