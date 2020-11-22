using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Core.QueryStore;
using Todo.Core.QueryStore.QueryModels;

namespace Todo.Infrastructure.QueryStore
{
    public class QueryStoreClient : IQueryStoreClient
    {
        private readonly IQueryStore _queryStore;
        private readonly IQueryStoreHistory _queryStoreCommandHistory;

        public QueryStoreClient(IQueryStore queryStore, IQueryStoreHistory queryStoreCommandHistory)
        {
            _queryStore = queryStore;
            _queryStoreCommandHistory = queryStoreCommandHistory;
        }

        public async Task<ItemListQueryModel> List(Guid? checkCommandId)
        {
            var queryModel = await _queryStore.Get("ItemList");

            if (queryModel == null) return new ItemListQueryModel();

            var hasCommandIdBeenProcessed = checkCommandId == null || await _queryStoreCommandHistory.Contains(checkCommandId.Value);

            var model = new ItemListQueryModel
            {
                LastUpdated = queryModel.LastUpdated,
                Items = JsonConvert.DeserializeObject<List<ItemQueryModel>>(queryModel.Data),
                AwaitingUpdate = !hasCommandIdBeenProcessed
            };

            return model;
        }
    }
}
