using Microsoft.AspNetCore.Mvc;

namespace RclubHook.Areas.Admin.Controllers;

public class EventItemsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}