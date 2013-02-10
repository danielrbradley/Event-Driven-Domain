namespace EventDrivenDomain.LocalFileStorage
{
    public class EventFilenameGenerator : IEventFilenameGenerator
    {
        private readonly string timestampFormatString;

        private readonly string fileExtension;

        public EventFilenameGenerator(string timestampFormatString, string fileExtension)
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