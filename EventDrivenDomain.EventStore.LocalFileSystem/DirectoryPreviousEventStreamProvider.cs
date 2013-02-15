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
            var eventFilePaths = Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly);
            // ReSharper disable PossibleMultipleEnumeration
            if (eventFilePaths.Any())
            {
                var previousFilePath = eventFilePaths.OrderBy(filePath => filePath).Last();
                // ReSharper restore PossibleMultipleEnumeration
                var previousFilename = Path.GetFileName(previousFilePath);
                if (previousFilename != null)
                {
                    var lastFilePath = Path.Combine(this.folderPath, previousFilename);
                    return File.OpenRead(lastFilePath);
                }
            }

            return null;
        }
    }
}