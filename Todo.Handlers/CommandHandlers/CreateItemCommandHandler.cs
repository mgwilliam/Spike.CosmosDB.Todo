using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Core.Commands;
using Todo.Core.Entities;
using Todo.Core.Events;
using Todo.Core.Messaging;
using Todo.Core.Repositories;

namespace Todo.Handlers.CommandHandlers
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IItemRepository _repository;
        private readonly IMessaging _messaging;

        public CreateItemCommandHandler(IItemRepository repository, IMessaging messaging)
        {
            _repository = repository;
            _messaging = messaging;
        }

        public async Task Handle(CreateItemCommand message, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Id = message.Id,
                Name = message.Name,
                Description = message.Description,
                Completed = false
            };

            await _repository.CreateItem(item);

            await _messaging.PublishEvent(new ItemsChangedEvent(message));
        }
    }
}
