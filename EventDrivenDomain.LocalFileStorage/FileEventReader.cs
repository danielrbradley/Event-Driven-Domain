namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;
    using System.Linq;

    public class FileEventReader<TBaseCommand> : IEventReader<TBaseCommand>, ILatestEventReader<TBaseCommand>
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

        public IEventEnumerable<TBaseCommand> Events
        {
            get
            {
                var searchPattern = this.GetSearchPattern();
                return
                    Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly)
                             .OrderBy(file => file)
                             .Select(file => this.eventFileReader.Read(Path.Combine(folderPath, file)))
                             .AsEventEnumerable();
            }
        }

        public Event<TBaseCommand> LatestEvent
        {
            get
            {
                var searchPattern = this.GetSearchPattern();
                var lastFile =
                    Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly)
                             .OrderBy(file => file)
                             .LastOrDefault();
                if (lastFile == null)
                {
                    return null;
                }

                return this.eventFileReader.Read(Path.Combine(folderPath, lastFile));
            }
        }

        private string GetSearchPattern()
        {
            var searchPattern = string.Concat("*.", this.fileExtension);
            return searchPattern;
        }
    }
}