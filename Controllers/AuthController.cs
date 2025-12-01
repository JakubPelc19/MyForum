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
}