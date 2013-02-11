namespace EventDrivenDomain.EventStore.Streams.ProtoBuf
{
    using System.IO;

    using global::ProtoBuf.Meta;

    public class ProtoBufSerializer<T> : ISerializer<T>
    {
        private readonly RuntimeTypeModel model;

        public ProtoBufSerializer(RuntimeTypeModel model)
        {
            this.model = model;
        }

        public void SerializeToStream(Stream destination, T objectToWrite)
        {
            model.Serialize(destination, objectToWrite);
        }
    }
}
