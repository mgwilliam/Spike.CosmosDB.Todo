using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Jobs
{
    public class CustomJobActivator : IJobActivator
    {
        private readonly IServiceProvider _service;

        public CustomJobActivator(IServiceProvider service)
        {
            _service = service;
        }

        public T CreateInstance<T>()
        {
            return _service.GetService<T>();
        }
    }
}
