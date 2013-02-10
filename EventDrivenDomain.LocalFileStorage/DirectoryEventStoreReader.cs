namespace EventDrivenDomain.LocalFileStorage
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DirectoryEventStoreReader<TBaseCommand> : IEventStoreReader<TBaseCommand>
    {
        private readonly string folderPath;

        private readonly string fileExtension;

        private readonly IEventFileReader<TBaseCommand> eventFileReader;

        public DirectoryEventStoreReader(string folderPath, string fileExtension, IEventFileReader<TBaseCommand> eventFileReader)
        {
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
            this.eventFileReader = eventFileReader;
        }

        public IEnumerable<EventReadResult<TBaseCommand>> EventReadResults
        {
            get
            {
                var searchPattern = string.Concat("*.", this.fileExtension);
                return
                    Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly)
                             .AsParallel()
                             .OrderBy(file => file)
                             .Select(file => this.eventFileReader.Read(Path.Combine(folderPath, file)));
            }
        }
    }
}