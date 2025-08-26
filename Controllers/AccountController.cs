using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RclubHook.Models;

namespace RclubHook.Controllers;

[Authorize]
public class AccountController: Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr)
    {
        _userManager = userMgr;
        _signInManager = signinMgr;
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        ViewBag.returnUrl = returnUrl;
        return View(new LoginViewModel());
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
    {


        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(
                    user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // ПРОВЕРЯЕМ РОЛИ ПОЛЬЗОВАТЕЛЯ
                    var userRoles = await _userManager.GetRolesAsync(user);
                    Console.WriteLine($"✅ User: {user.UserName}, Roles: {string.Join(", ", userRoles)}");

                    // Если пользователь admin и пытается в админку
                    if (userRoles.Contains("admin") && !string.IsNullOrEmpty(returnUrl) && returnUrl.Contains("/admin"))
                    {
                        return Redirect(returnUrl);
                    }
                    // Если admin но returnUrl не указан
                    else if (userRoles.Contains("admin") && string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    // Если не admin но пытается в админку
                    else if (!userRoles.Contains("admin") && !string.IsNullOrEmpty(returnUrl) &&
                             returnUrl.Contains("/admin"))
                    {
                        return RedirectToAction("AccessDenied", new { returnUrl });
                    }

                    return Redirect(returnUrl ?? "/");
                }
            }

            ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
        }

        return View(model);
    }
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    
    
    
    
    [AllowAnonymous]
    public IActionResult AccessDenied(string returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }
}