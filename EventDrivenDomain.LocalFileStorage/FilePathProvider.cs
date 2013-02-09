namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public class FilePathProvider : IFilePathProvider
    {
        private readonly string folderPath;

        private readonly string timestampFormatString;

        private readonly string fileExtension;

        public FilePathProvider(string folderPath, string timestampFormatString, string fileExtension)
        {
            this.folderPath = folderPath;
            this.timestampFormatString = timestampFormatString;
            this.fileExtension = fileExtension;
        }

        public string GetFilePath<T>(Event<T> eventToWrite)
        {
            var timestamp = eventToWrite.Timestamp.ToString(timestampFormatString);
            var filename = string.Concat(timestamp, ".", fileExtension);
            return Path.Combine(this.folderPath, filename);
        }
    }
}