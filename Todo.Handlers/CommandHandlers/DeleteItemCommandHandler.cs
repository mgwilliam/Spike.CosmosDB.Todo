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
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IItemRepository _repository;
        private readonly IMessaging _messaging;

        public DeleteItemCommandHandler(IItemRepository repository, IMessaging messaging)
        {
            _repository = repository;
            _messaging = messaging;
        }

        public async Task Handle(DeleteItemCommand message, CancellationToken cancellationToken)
        {
            await _repository.DeleteItem(message.Id);

            await _messaging.PublishEvent(new ItemsChangedEvent(message));
        }
    }
}
