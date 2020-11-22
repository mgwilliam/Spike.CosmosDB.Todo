using System;
using System.Threading.Tasks;

namespace Todo.Core.QueryStore
{
    /// <summary>
    /// reponsible for updating the query store
    /// </summary>
    public interface IQueryStoreUpdater
    {
        Task UpdateList(Guid sourceCommandId);
    }
}
