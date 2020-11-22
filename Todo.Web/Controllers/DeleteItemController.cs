using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.Commands;
using Todo.Core.Messaging;
using Todo.Core.Repositories;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class DeleteItemController : Controller
    {
        private readonly IItemRepository _repository;
        private readonly IMessaging _messaging;

        public DeleteItemController(IItemRepository repository, IMessaging messaging)
        {
            _repository = repository;
            _messaging = messaging;
        }

        [Route("Delete", Name = "DeleteItem")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var item = await _repository.GetItem(new Guid(id));

            if (item == null)
            {
                return NotFound();
            }

            return View(item.ToViewModel());
        }

        [HttpPost("Delete")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed([Bind("Id")] string id)
        {
            var command = new DeleteItemCommand {Id = new Guid(id)};

            await _messaging.SendCommand(command);

            TempData["message"] = "Item deleted";

            return RedirectToRoute("ListItems", new {lastCommandId = command.CommandId});
        }
    }
}
