using System;
using System.Threading.Tasks;
using Todo.Core.QueryStore.QueryModels;

namespace Todo.Core.QueryStore
{
    /// <summary>
    /// responsible for interacting with the query store
    /// </summary>
    public interface IQueryStoreClient
    {
        Task<ItemListQueryModel> List(Guid? checkCommandId);
    }
}
