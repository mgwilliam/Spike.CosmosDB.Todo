using System;

namespace Todo.Core.Messaging
{
    public interface IEvent
    {
        string SourceCommandId { get; }
    }
}
