using System;

namespace Todo.Infrastructure.Mongo
{
    public class MongoDbConnectionDetails
    {
        public string User { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }
    }
}
