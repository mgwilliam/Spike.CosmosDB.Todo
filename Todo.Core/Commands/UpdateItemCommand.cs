using System;
using MediatR;
using Todo.Core.Entities;
using Todo.Core.Messaging;

namespace Todo.Core.Commands
{
    public class UpdateItemCommand : CommandBase, ICommand, IRequest
    {
        public Item Item { get; set; }
    }
}
