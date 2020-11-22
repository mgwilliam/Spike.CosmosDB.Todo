using System;
using System.Threading.Tasks;

namespace Todo.Core.QueryStore
{
    /// <summary>
    /// responsible for storing query data
    /// </summary>
    public interface IQueryStore
    {
        Task<QueryStoreItem> Get(string key);

        Task Upsert(string key, object data);
    }
}
