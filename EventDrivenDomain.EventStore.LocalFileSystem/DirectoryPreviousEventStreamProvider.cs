namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    using System.IO;
    using System.Linq;

    using EventDrivenDomain.EventStore.Streams;

    public class DirectoryPreviousEventStreamProvider : IEventStreamProvider
    {
        private readonly string folderPath;

        private readonly string fileExtension;

        public DirectoryPreviousEventStreamProvider(string folderPath, string fileExtension)
        {
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
        }

        public Stream GetPreviousEventStream()
        {
            var searchPattern = string.Concat("*.", this.fileExtension);
            var eventFilenames = Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly);
            // ReSharper disable PossibleMultipleEnumeration
            if (eventFilenames.Any())
            {
                var lastFilename = eventFilenames.OrderBy(filename => filename).Last();
                // ReSharper restore PossibleMultipleEnumeration
                var lastFilePath = Path.Combine(this.folderPath, lastFilename);
                return File.OpenRead(lastFilePath);
            }

            return null;
        }
    }
}