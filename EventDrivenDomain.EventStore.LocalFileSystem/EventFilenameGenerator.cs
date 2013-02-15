namespace EventDrivenDomain.EventStore.LocalFileSystem
{
    public class EventFilenameGenerator : IEventFilenameGenerator
    {
        private readonly string timestampFormatString;

        private readonly string fileExtension;

        public const string DefaultTimestampFormatString = "yyyy'-'MM'-'dd'T'HH'-'mm'-'ss'-'fffffff";

        public const string DefaultFileExtension = "evt";

        public EventFilenameGenerator(string timestampFormatString = DefaultTimestampFormatString, string fileExtension = DefaultFileExtension)
        {
            this.timestampFormatString = timestampFormatString;
            this.fileExtension = fileExtension;
        }

        public string CreateFilename<T>(Event<T> eventToWrite)
        {
            var timestamp = eventToWrite.Timestamp.ToString(timestampFormatString);
            var filename = string.Concat(timestamp, ".", fileExtension);
            return filename;
        }
    }
}