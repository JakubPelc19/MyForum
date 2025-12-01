using Microsoft.AspNetCore.Mvc;

namespace MyForum.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(UserLoginViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserRegisterViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        return RedirectToAction("Login");
    }
}