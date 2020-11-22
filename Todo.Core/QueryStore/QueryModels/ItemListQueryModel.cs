using System;
using System.Collections.Generic;

namespace Todo.Core.QueryStore.QueryModels
{
    public class ItemListQueryModel
    {
        public ItemListQueryModel()
        {
            Items = new List<ItemQueryModel>();
        }

        public string LastUpdated { get; set; }

        public IEnumerable<ItemQueryModel> Items { get; set; }

        public bool AwaitingUpdate { get; set; }
    }
}
