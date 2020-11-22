using System;
using System.Threading.Tasks;
using MediatR;
using Todo.Core.Messaging;

namespace Todo.Infrastructure.Messaging
{
    public class MediatrMessaging : IMessaging
    {
        private readonly IMediator _mediator;

        public MediatrMessaging(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommand(ICommand command)
        {
            var request = command as IRequest;

            await _mediator.Send(request);
        }

        public async Task PublishEvent(IEvent @event)
        {
            var notification = @event as INotification;

            await _mediator.Publish(notification);
        }
    }
}
