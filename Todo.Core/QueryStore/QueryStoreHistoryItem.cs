using System;

namespace Todo.Core.QueryStore
{
    public class QueryStoreHistoryItem
    {
        public string CommandId { get; set; }

        public string WhenAdded { get; set; }
    }
}
