using System;

namespace Todo.Core.QueryStore.QueryModels
{
    public class ItemQueryModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}
