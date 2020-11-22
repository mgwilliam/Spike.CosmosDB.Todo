using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.Commands;
using Todo.Core.Messaging;
using Todo.Core.Repositories;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class EditItemController : Controller
    {
        private readonly IItemRepository _repository;
        private readonly IMessaging _messaging;

        public EditItemController(IItemRepository repository, IMessaging messaging)
        {
            _repository = repository;
            _messaging = messaging;
        }

        [Route("Edit", Name = "EditItem")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return BadRequest();

            var item = await _repository.GetItem(new Guid(id));

            if (item == null) return NotFound();

            return View(item.ToViewModel());
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Name,Description,Completed")] ItemViewModel item)
        {
            if (!ModelState.IsValid) return View(item);

            var command = new UpdateItemCommand {Item = item.ToEntity()};

            await _messaging.SendCommand(command);

            TempData["message"] = "Item updated";

            return RedirectToRoute("ListItems", new {lastCommandId = command.CommandId});
        }
    }
}
