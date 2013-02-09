namespace EventDrivenDomain.LocalFileStorage
{
    using System.IO;

    public class FileNamer : IFileNamer
    {
        private readonly string folderPath;

        private readonly string timestampFormatString;

        private readonly string fileExtension;

        public FileNamer(string folderPath, string timestampFormatString, string fileExtension)
        {
            this.folderPath = folderPath;
            this.timestampFormatString = timestampFormatString;
            this.fileExtension = fileExtension;
        }

        public string GetFilename<T>(Event<T> eventToWrite)
        {
            var timestamp = eventToWrite.Timestamp.ToString(timestampFormatString);
            var filename = string.Concat(timestamp, ".", fileExtension);
            return Path.Combine(this.folderPath, filename);
        }
    }
}