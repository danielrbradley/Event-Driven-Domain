namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    using System;
    using System.IO;
    using System.Threading;

    public class DirectoryEventStoreWriteLock : IEventStoreWriteLock
    {
        private readonly string lockFilePath;

        private readonly int millisecondlockWaitSleep;

        public const string DefaultLockFilename = "eventstore.lock";

        public const int DefaultMillisecondLockWaitSleep = 10000;

        public DirectoryEventStoreWriteLock(
            string path,
            string lockFilename = DefaultLockFilename,
            int millisecondlockWaitSleep = DefaultMillisecondLockWaitSleep)
        {
            this.millisecondlockWaitSleep = millisecondlockWaitSleep;
            this.lockFilePath = Path.Combine(path, lockFilename);
        }

        public IDisposable WaitAquire()
        {
            var aquired = false;
            while (!aquired)
            {
                if (!File.Exists(lockFilePath))
                {
                    try
                    {
                        using (var file = File.Open(this.lockFilePath, FileMode.CreateNew))
                        {
                        }

                        aquired = true;
                        var writeLock = new DirectoryWriteLock(this.lockFilePath);
                        return writeLock;
                    }
                    catch (IOException)
                    {
                    }
                }

                Thread.Sleep(this.millisecondlockWaitSleep);
            }

            throw new WriteLockAcquisitionException("Failed to aquire directory write lock");
        }

        internal class DirectoryWriteLock : IDisposable
        {
            private readonly string lockFilePath;

            public DirectoryWriteLock(string lockFilePath)
            {
                this.lockFilePath = lockFilePath;
            }

            public void Dispose()
            {
                File.Delete(this.lockFilePath);
            }
        }
    }
}
