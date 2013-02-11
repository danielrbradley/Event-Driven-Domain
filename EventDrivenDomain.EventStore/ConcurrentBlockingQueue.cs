namespace EventDrivenDomain.EventStore
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    public class ConcurrentBlockingQueue<T> : IEnumerable<T>, ICollection
    {
        private readonly CancellationToken cancellationToken;

        private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        private readonly ManualResetEvent reset = new ManualResetEvent(false);

        public ConcurrentBlockingQueue(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            cancellationToken.Register(() => this.reset.Set());
        }

        public bool TryDequeue(out T result)
        {
            return queue.TryDequeue(out result);
        }

        public T WaitDequeue()
        {
            T result;

            var success = this.queue.TryDequeue(out result);

            while (!success)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return result;
                }

                success = queue.TryDequeue(out result);

                if (success)
                {
                    continue;
                }

                this.reset.Reset();
                success = this.queue.TryDequeue(out result);

                if (success)
                {
                    this.reset.Set();
                }
                else
                {
                    this.reset.WaitOne();
                }
            }

            return result;
        }

        public bool TryPeek(out T result)
        {
            return queue.TryPeek(out result);
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
            this.reset.Set();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)queue).CopyTo(array, index);
        }

        int ICollection.Count
        {
            get
            {
                return queue.Count;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)queue).SyncRoot;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection)queue).IsSynchronized;
            }
        }
    }
}
