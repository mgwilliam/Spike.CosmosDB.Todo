using System;
using Todo.Core.Entities;

namespace Todo.Core.QueryStore.QueryModels
{
    public static class ItemEntityExtensions
    {
        public static ItemQueryModel ToQueryModel(this Item entity)
        {
            return new ItemQueryModel
            {
                Id = entity.Id,
                Completed = entity.Completed,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
