using System;
using MediatR;
using Todo.Core.Messaging;

namespace Todo.Core.Events
{
    //note: dependency on mediatr in the core project is undesirable (but not easily avoided)
    public class ItemsChangedEvent : EventBase, INotification
    {
        public ItemsChangedEvent(CommandBase command) : base(command) { }
    }
}
