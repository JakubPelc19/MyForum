using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyForum.Controllers;

public class AuthController(AppDbContext _context) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel user)
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
    public async Task<IActionResult> Register(UserRegisterViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        var collision = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email || u.Username == user.Username);

        if (collision is not null)
            return View(user);
        
        

        User newUser = new User();

        var passwordhasher = new PasswordHasher<User>();
        var hashedpassword = passwordhasher.HashPassword(newUser, user.Password);


        newUser.Email = user.Email;
        newUser.Username = user.Username;
        newUser.PasswordHash = hashedpassword;

        await _context.Users.AddAsync(newUser);

        await _context.SaveChangesAsync();

        return RedirectToAction("Login");
    }
}