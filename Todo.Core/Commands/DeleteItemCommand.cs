using System;
using MediatR;
using Todo.Core.Messaging;

namespace Todo.Core.Commands
{
    public class DeleteItemCommand : CommandBase, ICommand, IRequest
    {
        public Guid Id { get; set; }
    }
}
