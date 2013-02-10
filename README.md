# Event Driven Domain
This library is aimed at making it easier to build event driven domain objects which can use immutable internal state objects.

## Event Store Backed Aggregate Root
The EventStoreBackedAggregateRoot class is designed to encapsulate the complexity of taming a multi-threaded outside world into a serialised queue of messages which can be written to an event store before completing the message.

## Example Protobuf Files Event Store Stack

                              -----------EventStore------------
                             /                |                \
         EventStoreEnumerator         TimestampProvider     EventStoreWriter
               |                                                   |
    DirectoryEventStoreReader                           DirectoryEventStoreWriter
               |                                       /             |           \
         EventFileReader                    EventFileWriter  EventStoreWriteLock  EventFilenameGenerator 
               |                                    |
      HashedEventStreamReader              StreamEventWriter
               |                          /                 \
    ProtobufEventStreamDecoder  ProtobufStreamEncoder  SequenceValidationTranscoderAdapterFactory  
                                                                 |                                 \
                                                     HashedStreamIntegretyTranscoderAdapterFactory  PreviousEventHashReader
