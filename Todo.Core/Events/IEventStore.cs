using System;
using System.Threading.Tasks;
using Todo.Core.Messaging;

namespace Todo.Core.Events
{
    /// <summary>
    /// responsible for persisting domain events
    /// </summary>
    public interface IEventStore
    {
        Task Add(IEvent @event);
    }
}
