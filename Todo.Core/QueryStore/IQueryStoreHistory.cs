using System;
using System.Threading.Tasks;

namespace Todo.Core.QueryStore
{
    /// <summary>
    /// used to record a history of commands that have been processed
    /// </summary>
    public interface IQueryStoreHistory
    {
        Task Add(Guid commandId);

        Task<bool> Contains(Guid commandId);
    }
}
