using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Core.Entities;

namespace Todo.Core.Repositories
{
    /// <summary>
    /// master data store
    /// </summary>
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItems();

        Task CreateItem(Item item);

        Task UpdateItem(Guid itemId, Item item);

        Task<Item> GetItem(Guid itemId);

        Task DeleteItem(Guid itemId);
    }
}
