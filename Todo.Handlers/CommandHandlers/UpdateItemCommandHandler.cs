using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Core.Commands;
using Todo.Core.Events;
using Todo.Core.Messaging;
using Todo.Core.Repositories;

namespace Todo.Handlers.CommandHandlers
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IItemRepository _repository;
        private readonly IMessaging _messaging;

        public UpdateItemCommandHandler(IItemRepository repository, IMessaging messaging)
        {
            _repository = repository;
            _messaging = messaging;
        }

        public async Task Handle(UpdateItemCommand message, CancellationToken cancellationToken)
        {
            await _repository.UpdateItem(message.Item.Id, message.Item);

            await _messaging.PublishEvent(new ItemsChangedEvent(message));
        }
    }
}
