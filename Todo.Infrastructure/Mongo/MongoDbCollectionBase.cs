using System;
using System.Security.Authentication;
using MongoDB.Driver;

namespace Todo.Infrastructure.Mongo
{
    public abstract class MongoDbCollectionBase
    {
        private readonly string _dbName;
        private readonly string _collectionName;
        private readonly MongoDbConnectionDetails _details;

        protected MongoDbCollectionBase(string dbName, string collectionName, MongoDbConnectionDetails details)
        {
            _dbName = dbName;
            _collectionName = collectionName;
            _details = details;
        }

        protected IMongoCollection<T> GetCollection<T>()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(_details.Host, 10255),
                UseSsl = true,
                SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12}
            };

            MongoIdentity identity = new MongoInternalIdentity(_dbName, _details.User);
            MongoIdentityEvidence evidence = new PasswordEvidence(_details.Password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            var client = new MongoClient(settings);
            var database = client.GetDatabase(_dbName);
            var collection = database.GetCollection<T>(_collectionName);

            return collection;
        }
    }
}
