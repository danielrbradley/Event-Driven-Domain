namespace EventDrivenDomain.EventStore.IntegrationTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;

    using EventDrivenDomain.EventStore.LocalFileSystem;
    using EventDrivenDomain.EventStore.Streams;
    using EventDrivenDomain.EventStore.Streams.ProtoBuf;

    using NUnit.Framework;

    using ProtoBuf.Meta;

    using IUserCommand = EventDrivenDomain.EventStore.IntegrationTests.Users.IUserCommand;
    using UpdateName = EventDrivenDomain.EventStore.IntegrationTests.Users.UpdateName;
    using User = EventDrivenDomain.EventStore.IntegrationTests.Users.User;
    using UserAggregate = EventDrivenDomain.EventStore.IntegrationTests.Users.UserAggregate;

    public class Protobuf_LocalFileSystem_Stream_AggregateRoot_Tests
    {
        private const string EventStorePath = "temp_event_store";

        [TestFixtureSetUp]
        public void Setup()
        {
            Directory.CreateDirectory(EventStorePath);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            Directory.Delete(EventStorePath, true);
        }

        [Test]
        public void ChangeUserName()
        {
            var user = ConstructUserAggregateRoot();
            user.ChangeName("New Name");
        }

        private static User ConstructUserAggregateRoot()
        {
            var eventStore = ContructEventStore();
            var initialState = new UserAggregate();
            var user = new User(eventStore, initialState);
            return user;
        }

        private static IWritableEventStore<IUserCommand> ContructEventStore()
        {
            var timestampProvider = new SystemClockUtcTimestampProvider();
            var eventStoreWriter = ConstructEventStoreWriter();
            var events = new List<Event<IUserCommand>>();

            var eventStore = new EventStore<IUserCommand>(
                timestampProvider, eventStoreWriter, events);
            return eventStore;
        }

        private static IEventStoreWriter<IUserCommand> ConstructEventStoreWriter()
        {
            var filePathProvider = new EventFilenameGenerator();
            var writeLock = new DirectoryEventStoreWriteLock(EventStorePath);
            var eventFileWriter = ConstructEventFileWriter();
            var eventStoreWriter = new DirectoryEventStoreWriter<IUserCommand>(
                EventStorePath, filePathProvider, writeLock, eventFileWriter);
            return eventStoreWriter;
        }

        private static IEventFileWriter<IUserCommand> ConstructEventFileWriter()
        {
            var serializer = ConstructSerializer();
            var hashChecksumTranscodingStreamFactory = ConstructTranscodingStreamFactories();
            var streamEventWriter = new StreamEventWriter<IUserCommand>(
                serializer, hashChecksumTranscodingStreamFactory);
            var eventFileWriter = new EventFileWriter<IUserCommand>(streamEventWriter);
            return eventFileWriter;
        }

        private static ITranscodingStreamFactory ConstructTranscodingStreamFactories()
        {
            var hashAlgorithm = SHA256.Create();
            var streamHashGenerator = new CryptoStreamHashGenerator(hashAlgorithm);
            var hashByteCount = streamHashGenerator.GetHashSize();
            var previousEventStreamProvider = new DirectoryPreviousEventStreamProvider(
                EventStorePath, EventFilenameGenerator.DefaultFileExtension);
            var previousEventHashReader = new PreviousEventStreamHashReader(
                previousEventStreamProvider, hashByteCount);
            var sequenceValidationTranscodingStreamFactor =
                new SequenceValidationTranscodingStreamFactory(previousEventHashReader);
            var hashChecksumTranscodingStreamFactory =
                new HashChecksumTranscodingStreamFactory(streamHashGenerator, sequenceValidationTranscodingStreamFactor);
            return hashChecksumTranscodingStreamFactory;
        }

        private static ISerializer<Event<IUserCommand>> ConstructSerializer()
        {
            var protoBufModel = TypeModel.Create();
            protoBufModel.Add(typeof(IUserCommand), true).AddSubType(1, typeof(UpdateName));
            var serializer = new ProtoBufSerializer<Event<IUserCommand>>(protoBufModel);
            return serializer;
        }
    }
}
