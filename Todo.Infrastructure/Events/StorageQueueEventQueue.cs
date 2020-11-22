using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Todo.Core.Events;
using Todo.Core.Messaging;
using Todo.Infrastructure.StorageQueue;

namespace Todo.Infrastructure.Events
{
    public class StorageQueueEventQueue : IEventStore
    {
        private const string QueueName = "events";
        private readonly string _connectionString;

        public StorageQueueEventQueue(StorageQueueConnectionDetails details)
        {
            _connectionString = details.ConnectionString;
        }

        public async Task Add(IEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event, Formatting.Indented);

            var item = new EventItem
            {
                EventType = @event.GetType().Name,
                Data = json,
                SourceCommandId = @event.SourceCommandId
            };

            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            var client = storageAccount.CreateCloudQueueClient();
            var queue = client.GetQueueReference(QueueName);

            var message = new CloudQueueMessage(JsonConvert.SerializeObject(item, Formatting.Indented));

            await queue.AddMessageAsync(message);
        }
    }
}
