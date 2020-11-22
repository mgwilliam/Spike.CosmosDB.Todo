using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Core.Events;
using Todo.Core.Messaging;
using Todo.Core.QueryStore;

namespace Todo.Handlers.EventHandlers
{
    public class ItemsChangedEventHandler : INotificationHandler<ItemsChangedEvent>
    {
        private readonly IQueryStoreUpdater _queryStoreUpdater;
        private readonly IEventStore _eventStore;

        public ItemsChangedEventHandler(IQueryStoreUpdater queryStoreUpdater, IEventStore eventStore)
        {
            _queryStoreUpdater = queryStoreUpdater;
            _eventStore = eventStore;
        }

        public async Task Handle(ItemsChangedEvent notification, CancellationToken cancellationToken)
        {
            //await HandleUsingEventStore(notification);

            await HandleInProcess(notification);
        }

        //private async Task HandleUsingEventStore(IEvent @event)
        //{
        //    await _eventStore.Add(@event);
        //}

        private async Task HandleInProcess(IEvent @event)
        {
            await _queryStoreUpdater.UpdateList(new Guid(@event.SourceCommandId));
        }
    }
}
