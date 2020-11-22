using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Newtonsoft.Json;
using Todo.Core.QueryStore;
using Todo.Infrastructure.Mongo;

namespace Todo.Infrastructure.QueryStore
{
    public class MongoDbQueryStore : MongoDbCollectionBase, IQueryStore
    {
        private const string Database = "Todos";
        private const string Collection = "ViewData";

        public MongoDbQueryStore(MongoDbConnectionDetails details) : base(Database, Collection, details) { }

        public async Task<QueryStoreItem> Get(string key)
        {
            var collection = GetCollection<QueryStoreItem>();
            var filter = Builders<QueryStoreItem>.Filter.Eq(s => s.Id, key);

            var result = await collection.FindAsync(filter);

            return result.SingleOrDefault();
        }

        public async Task Upsert(string key, object data)
        {
            var collection = GetCollection<QueryStoreItem>();

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            var item = new QueryStoreItem
            {
                Id = key,
                LastUpdated = DateTime.Now.ToString("F"),
                Data = json
            };

            await collection.ReplaceOneAsync(d => d.Id == key, item, new UpdateOptions {IsUpsert = true});
        }
    }
}
