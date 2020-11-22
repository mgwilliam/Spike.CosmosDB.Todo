using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Core.Events;
using Todo.Core.Messaging;
using Todo.Infrastructure.Mongo;

namespace Todo.Infrastructure.Events
{
    public class MongoDbEventStore : MongoDbCollectionBase, IEventStore
    {
        private const string Database = "Todos";
        private const string Collection = "Events";

        public MongoDbEventStore(MongoDbConnectionDetails details) : base(Database, Collection, details) { }

        public async Task Add(IEvent @event)
        {
            var collection = GetCollection<EventItem>();

            var json = JsonConvert.SerializeObject(@event, Formatting.Indented);

            var item = new EventItem
            {
                EventType = @event.GetType().Name,
                Data = json,
                SourceCommandId = @event.SourceCommandId
            };

            await collection.InsertOneAsync(item);
        }
    }
}
