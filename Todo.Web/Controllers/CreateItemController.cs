using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.Commands;
using Todo.Core.Messaging;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class CreateItemController : Controller
    {
        private readonly IMessaging _messaging;

        public CreateItemController(IMessaging messaging)
        {
            _messaging = messaging;
        }

        [Route("Create", Name = "CreateItem")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Description")] ItemViewModel item)
        {
            if (!ModelState.IsValid) return View(item);

            var command = new CreateItemCommand {Id = Guid.NewGuid(), Name = item.Name, Description = item.Description};

            await _messaging.SendCommand(command);

            TempData["message"] = "New item added";

            return RedirectToRoute("ListItems", new {lastCommandId = command.CommandId});
        }
    }
}
