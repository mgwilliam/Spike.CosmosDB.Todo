using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Core.Events;
using Todo.Core.Messaging;
using Todo.Core.QueryStore;
using Todo.Core.Repositories;
using Todo.Handlers.CommandHandlers;
using Todo.Infrastructure.Events;
using Todo.Infrastructure.Messaging;
using Todo.Infrastructure.Mongo;
using Todo.Infrastructure.QueryStore;
using Todo.Infrastructure.Repositories;
using Todo.Infrastructure.StorageQueue;

namespace Todo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });

            services.AddTransient<IQueryStoreClient, QueryStoreClient>();
            services.AddTransient<IMessaging, MediatrMessaging>();
            services.AddSingleton<IItemRepository, MongoDbItemRepository>();
            services.AddSingleton<IQueryStoreUpdater, QueryStoreUpdater>();
            services.AddSingleton<IQueryStore, MongoDbQueryStore>();
            services.AddSingleton<IEventStore, MongoDbEventStore>();
            //services.AddSingleton<IEventStore, StorageQueueEventQueue>();
            services.AddSingleton<IQueryStoreHistory, MongoDbQueryStoreHistory>();
            services.AddSingleton(Configuration.GetSection("MongoDbConnectionDetails").Get<MongoDbConnectionDetails>());
            services.AddSingleton(Configuration.GetSection("EventQueueConnectionDetails").Get<StorageQueueConnectionDetails>());

            services.AddMvc();

            services.AddMediatR(typeof(CreateItemCommandHandler).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = new RewriteOptions()
                .AddRedirectToHttps();

            app.UseRewriter(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); //todo: this doesn't exist
            }

            app.UseStaticFiles();

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Item}/{action=Index}/{id?}"); });
        }
    }
}
