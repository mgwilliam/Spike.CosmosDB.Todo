using System;

namespace Todo.Core.Messaging
{
    public abstract class EventBase : IEvent
    {
        protected EventBase(CommandBase command)
        {
            SourceCommandId = command.CommandId.ToString();
        }

        public string SourceCommandId { get; }
    }
}
