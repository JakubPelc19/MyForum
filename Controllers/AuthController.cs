using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            return View(user);

        var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        
        if (foundUser is null)
            return View(user);
        
        var passwordhasher = new PasswordHasher<User>();

        if (passwordhasher.VerifyHashedPassword(foundUser, foundUser.PasswordHash, user.Password) == PasswordVerificationResult.Failed)
            return View(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, foundUser.Username),
            new Claim(ClaimTypes.Email, foundUser.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow,

        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);


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
            return View(user);
        

        var collision = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email || u.Username == user.Username);

        if (collision is not null)    
            return View(user);
        
            
        
        
        if (user.Password != user.ConfirmPassword)    
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