namespace EventDrivenDomain.LocalFileStorage
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DirectorySequenceValidatableEventEnumerable<TBaseCommand> : IEnumerable<SequenceValidatableEvent<TBaseCommand>>
    {
        private readonly string folderPath;

        private readonly string fileExtension;

        private readonly ISequenceValidatableEventFileReader<TBaseCommand> sequenceValidatableEventFileReader;

        public DirectorySequenceValidatableEventEnumerable(string folderPath, string fileExtension, ISequenceValidatableEventFileReader<TBaseCommand> sequenceValidatableEventFileReader)
        {
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
            this.sequenceValidatableEventFileReader = sequenceValidatableEventFileReader;
        }

        private IEnumerable<SequenceValidatableEvent<TBaseCommand>> InnerEnumerable
        {
            get
            {
                var searchPattern = string.Concat("*.", this.fileExtension);
                return
                    Directory.EnumerateFiles(this.folderPath, searchPattern, SearchOption.TopDirectoryOnly)
                             .AsParallel()
                             .OrderBy(file => file)
                             .Select(file => this.sequenceValidatableEventFileReader.Read(Path.Combine(folderPath, file)));
            }
        }

        public IEnumerator<SequenceValidatableEvent<TBaseCommand>> GetEnumerator()
        {
            return InnerEnumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}