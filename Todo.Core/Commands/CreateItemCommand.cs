using System;
using MediatR;
using Todo.Core.Messaging;

namespace Todo.Core.Commands
{
    public class CreateItemCommand : CommandBase, ICommand, IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
