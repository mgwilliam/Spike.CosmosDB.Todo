using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Todo.Core.QueryStore;
using Todo.Infrastructure.Mongo;

namespace Todo.Infrastructure.QueryStore
{
    public class MongoDbQueryStoreHistory : MongoDbCollectionBase, IQueryStoreHistory
    {
        private const string Database = "Todos";
        private const string Collection = "ProcessedCommands";

        public MongoDbQueryStoreHistory(MongoDbConnectionDetails details) : base(Database, Collection, details) { }

        public async Task Add(Guid commandId)
        {
            var collection = GetCollection<QueryStoreHistoryItem>();

            var item = new QueryStoreHistoryItem
            {
                CommandId = commandId.ToString(),
                WhenAdded = DateTime.Now.ToString("F")
            };

            await collection.ReplaceOneAsync(d => d.CommandId == item.CommandId, item, new UpdateOptions {IsUpsert = true});
        }

        public async Task<bool> Contains(Guid commandId)
        {
            var findCommandId = commandId.ToString();
            var collection = GetCollection<QueryStoreHistoryItem>();
            var filter = Builders<QueryStoreHistoryItem>.Filter.Eq(s => s.CommandId, findCommandId);

            var result = await collection.CountAsync(filter);

            return result > 0;
        }
    }
}
