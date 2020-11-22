using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.QueryStore;
using Todo.Core.QueryStore.QueryModels;
using Todo.Core.Repositories;

namespace Todo.Infrastructure.QueryStore
{
    public class QueryStoreUpdater : IQueryStoreUpdater
    {
        private readonly IQueryStore _queryStore;
        private readonly IQueryStoreHistory _queryStoreCommandHistory;
        private readonly IItemRepository _repository;

        public QueryStoreUpdater(IQueryStore queryStore, IQueryStoreHistory queryStoreCommandHistory, IItemRepository repository)
        {
            _queryStore = queryStore;
            _queryStoreCommandHistory = queryStoreCommandHistory;
            _repository = repository;
        }

        public async Task UpdateList(Guid sourceCommandId)
        {
            var items = _repository.GetAllItems().Result;
            var queryItems = items.Select(i => i.ToQueryModel());

            await _queryStore.Upsert("ItemList", queryItems);
            await _queryStoreCommandHistory.Add(sourceCommandId);
        }
    }
}
