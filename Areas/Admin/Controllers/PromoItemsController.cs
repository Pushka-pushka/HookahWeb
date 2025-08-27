using Microsoft.AspNetCore.Mvc;

namespace RclubHook.Areas.Admin.Controllers;

public class PromoItemsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}