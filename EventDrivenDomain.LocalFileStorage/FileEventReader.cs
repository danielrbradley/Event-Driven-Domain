namespace EventDrivenDomain.LocalFileStorage
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileEventReader<TBaseCommand> : IEventReader<TBaseCommand>
    {
        private readonly string folderPath;

        private readonly string fileExtension;

        private readonly IEventFileReader<TBaseCommand> eventFileReader;

        public FileEventReader(string folderPath, string fileExtension, IEventFileReader<TBaseCommand> eventFileReader)
        {
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
            this.eventFileReader = eventFileReader;
        }

        public IEnumerable<Event<TBaseCommand>> Events
        {
            get
            {
                var searchPattern = string.Concat("*.", this.fileExtension);
                var fileResults =
                    Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly)
                             .AsParallel()
                             .OrderBy(file => file)
                             .Select(file => this.eventFileReader.Read(Path.Combine(folderPath, file)));

                Hash previousHash = Hash.None;
                bool isFirst = true;
                foreach (var eventReadResult in fileResults)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else if (previousHash != eventReadResult.PreviousHash)
                    {
                        throw new EventStoreCorruptionException("Event sequence hash mismatch");
                    }

                    yield return eventReadResult.Event;

                    previousHash = eventReadResult.Hash;
                }
            }
        }
    }
}