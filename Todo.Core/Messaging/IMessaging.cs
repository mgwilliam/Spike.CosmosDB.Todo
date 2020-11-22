using System;
using System.Threading.Tasks;

namespace Todo.Core.Messaging
{
    /// <summary>
    /// for sending commands and publishing events
    /// </summary>
    public interface IMessaging
    {
        Task SendCommand(ICommand command);

        Task PublishEvent(IEvent @event);
    }
}
