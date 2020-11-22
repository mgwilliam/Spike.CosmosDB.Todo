using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.QueryStore;

namespace Todo.Web.Controllers
{
    public class ListItemsController : Controller
    {
        private readonly IQueryStoreClient _queryStore;

        public ListItemsController(IQueryStoreClient queryStore)
        {
            _queryStore = queryStore;
        }

        [Route("", Name = "ListItems")]
        public async Task<IActionResult> Index(Guid? lastCommandId)
        {
            var listViewModel = await _queryStore.List(lastCommandId);

            return View(listViewModel);
        }
    }
}
