using System;

namespace Todo.Core.Messaging
{
    public abstract class CommandBase
    {
        protected CommandBase()
        {
            CommandId = Guid.NewGuid();
        }

        public Guid CommandId { get; }
    }
}
