using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Todo.Core.QueryStore;
using Todo.Infrastructure.Events;

namespace Todo.Jobs
{
    public class Triggers
    {
        private readonly IQueryStoreUpdater _queryStoreUpdater;

        public Triggers(IQueryStoreUpdater queryStoreUpdater)
        {
            _queryStoreUpdater = queryStoreUpdater;
        }

        //[FunctionName("TodoEventsCosmosDbTrigger")]
        //public void TodoEventsCosmosDbTrigger([CosmosDBTrigger("Todos", "Events", ConnectionStringSetting = "CosmosDBEventsKey", CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> documents, TraceWriter log)
        //{
        //    foreach (var document in documents)
        //    {
        //        try
        //        {
        //            var eventItem = JsonConvert.DeserializeObject<EventItem>(document.ToString());

        //            log.Info($"Processing {eventItem.EventType} event");

        //            HandleEvent(eventItem);
        //        }
        //        catch (Exception ex)
        //        {
        //            log.Error($"Error processing document {document.Id}", ex);
        //        }
        //    }
        //}

        [FunctionName("TodoEventsStorageQueueTrigger")]
        public void TodoEventsStorageQueueTrigger([QueueTrigger("events", Connection = "EventQueueConnectionString")] string message, TraceWriter log)
        {
            try
            {
                var eventItem = JsonConvert.DeserializeObject<EventItem>(message);

                log.Info($"Processing {eventItem.EventType} event");

                HandleEvent(eventItem);
            }
            catch (Exception ex)
            {
                log.Error("Error processing message", ex);
            }
        }

        private void HandleEvent(EventItem eventStoreItem)
        {
            //note: should handle events based on event type
            _queryStoreUpdater.UpdateList(new Guid(eventStoreItem.SourceCommandId));
        }
    }
}
