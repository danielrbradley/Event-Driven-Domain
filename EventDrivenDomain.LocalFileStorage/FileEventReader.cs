namespace EventDrivenDomain.LocalFileStorage
{
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

        public IEventEnumerable<TBaseCommand> Events
        {
            get
            {
                var searchPattern = string.Concat("*.", this.fileExtension);
                var state = new EventReadState();
                return
                    Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly)
                             .OrderBy(file => file)
                             .Select(file =>
                                 {
                                     string hash;
                                     var result = this.eventFileReader.Read(state, Path.Combine(folderPath, file), out hash);
                                     state.PreviousHash = hash;
                                     return result;
                                 })
                             .AsEventEnumerable();
            }
        }
    }
}