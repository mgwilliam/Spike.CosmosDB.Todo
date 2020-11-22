using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Todo.Core.Entities;
using Todo.Core.Repositories;
using Todo.Infrastructure.Mongo;

namespace Todo.Infrastructure.Repositories
{
    public class MongoDbItemRepository : MongoDbCollectionBase, IItemRepository
    {
        private const string Database = "Todos";
        private const string Collection = "Items";

        public MongoDbItemRepository(MongoDbConnectionDetails details) : base(Database, Collection, details) { }

        public async Task<List<Item>> GetAllItems()
        {
            var collection = GetCollection<Item>();
            var filter = Builders<Item>.Filter.Empty;

            var result = await collection.FindAsync(filter);

            return result.ToList();
        }

        public async Task CreateItem(Item item)
        {
            var collection = GetCollection<Item>();

            await collection.InsertOneAsync(item);
        }

        public async Task UpdateItem(Guid itemId, Item item)
        {
            var collection = GetCollection<Item>();
            var filter = Builders<Item>.Filter.Eq(s => s.Id, itemId);

            await collection.ReplaceOneAsync(filter, item);
        }

        public async Task<Item> GetItem(Guid itemId)
        {
            var collection = GetCollection<Item>();
            var filter = Builders<Item>.Filter.Eq(s => s.Id, itemId);

            var result = await collection.FindAsync(filter);

            return result.Single();
        }

        public async Task DeleteItem(Guid itemId)
        {
            var collection = GetCollection<Item>();
            var filter = Builders<Item>.Filter.Eq(s => s.Id, itemId);

            await collection.DeleteOneAsync(filter);
        }
    }
}
