using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RclubHook.Domain.Repositories;

namespace RclubHook.Areas.Admin.Controllers;
[Area("admin")]
[Authorize(Policy = "AdminPolicy")]
public class HomeController : Controller
{
    private readonly DataManager _dataManager;

    public HomeController(DataManager dataManager)
    {
        _dataManager = dataManager;
    }
    
    
    public IActionResult Index()
    {
        return View(_dataManager.ServiceItems.GetServiceItems());
        
    }
    
    // [AllowAnonymous]
    // public IActionResult Test()
    // {
    //     return View(_dataManager.ServiceItems.GetServiceItems());
    // }
}