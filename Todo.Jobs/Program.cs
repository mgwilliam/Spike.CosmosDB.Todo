using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Core.Events;
using Todo.Core.QueryStore;
using Todo.Core.Repositories;
using Todo.Infrastructure.Events;
using Todo.Infrastructure.Mongo;
using Todo.Infrastructure.QueryStore;
using Todo.Infrastructure.Repositories;

namespace Todo.Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var services = new ServiceCollection();

                services.AddSingleton<Triggers, Triggers>();
                services.AddSingleton<IQueryStoreUpdater, QueryStoreUpdater>();
                services.AddSingleton<IQueryStore, MongoDbQueryStore>();
                services.AddSingleton<IItemRepository, MongoDbItemRepository>();
                services.AddSingleton<IEventStore, MongoDbEventStore>();
                services.AddSingleton<IQueryStoreHistory, MongoDbQueryStoreHistory>();
                services.AddSingleton(configuration.GetSection("MongoDbConnectionDetails").Get<MongoDbConnectionDetails>());

                Environment.SetEnvironmentVariable("AzureWebJobsDashboard", configuration.GetConnectionString("WebJobsDashboard"));
                Environment.SetEnvironmentVariable("AzureWebJobsStorage", configuration.GetConnectionString("WebJobsStorage"));
                Environment.SetEnvironmentVariable("CosmosDbEventsKey", configuration.GetConnectionString("CosmosDbEventsKey"));
                Environment.SetEnvironmentVariable("EventQueueConnectionString", configuration.GetConnectionString("EventQueueConnectionString"));

                var config = new JobHostConfiguration
                {
                    JobActivator = new CustomJobActivator(services.BuildServiceProvider())
                };

                config.UseCosmosDB();

                if (config.IsDevelopment)
                {
                    config.UseDevelopmentSettings();
                }

                var host = new JobHost(config);

                host.RunAndBlock();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Console.WriteLine("\nPress a key to exit");
                Console.ReadKey();
            }
        }
    }
}
