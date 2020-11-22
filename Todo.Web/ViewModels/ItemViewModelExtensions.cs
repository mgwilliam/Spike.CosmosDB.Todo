using System;
using Todo.Core.Entities;

namespace Todo.Web.ViewModels
{
    public static class ItemViewModelExtensions
    {
        public static Item ToEntity(this ItemViewModel model)
        {
            return new Item
            {
                Id = model.Id,
                Completed = model.Completed,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static ItemViewModel ToViewModel(this Item entity)
        {
            return new ItemViewModel
            {
                Id = entity.Id,
                Completed = entity.Completed,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
