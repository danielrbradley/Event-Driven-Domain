namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;
    using System.Linq;

    public class EventReader<TBaseCommand> : IEventReader<TBaseCommand>, ILatestEventReader<TBaseCommand>
    {
        private readonly string folderPath;

        private readonly IEventFileReader<TBaseCommand> eventFileReader;

        public EventReader(string folderPath, IEventFileReader<TBaseCommand> eventFileReader)
        {
            this.folderPath = folderPath;
            this.eventFileReader = eventFileReader;
        }

        public IEventEnumerable<TBaseCommand> Events
        {
            get
            {
                return
                    Directory.EnumerateFiles(this.folderPath, "*.event", SearchOption.TopDirectoryOnly)
                             .OrderBy(file => file)
                             .Select(file => this.eventFileReader.Read(file))
                             .AsEventEnumerable();
            }
        }

        public Event<TBaseCommand> LatestEvent
        {
            get
            {
                var lastFile =
                    Directory.EnumerateFiles(this.folderPath, "*.event", SearchOption.TopDirectoryOnly)
                             .OrderBy(file => file)
                             .LastOrDefault();
                if (lastFile == null)
                {
                    return null;
                }

                return this.eventFileReader.Read(lastFile);
            }
        }
    }
}