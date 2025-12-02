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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginViewModel user)
    {
        if (!ModelState.IsValid)
            return View(user);

        var normalizedEmail = user.Email.Trim().ToLower();

        var foundUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == normalizedEmail);
        
        if (foundUser is null)
        {  
            ModelState.AddModelError("ErrorMessage", "Invalid credentials");
            return View(user);
        }
        
        var passwordHasher = new PasswordHasher<User>();
        if (passwordHasher.VerifyHashedPassword(foundUser, foundUser.PasswordHash, user.Password) == PasswordVerificationResult.Failed)
        {   
            ModelState.AddModelError("ErrorMessage", "Invalid credentials");
            return View(user);
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
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
            RedirectUri = "/"

        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);


        return Redirect("/");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterViewModel user)
    {
        if (!ModelState.IsValid)
            return View(user);
        
        var normalizedEmail = user.Email.Trim().ToLower();
        var normalizedUsername = user.Username.Trim().ToLower();
        
        var collisionUsername = await _context.Users.AnyAsync(u => u.Username == normalizedUsername);

        if (collisionUsername)    
        {  
            ModelState.AddModelError("Username", "Username is already taken");
            return View(user);
        }

        var collisionEmail = await _context.Users.AnyAsync(u => u.Email == normalizedEmail);

        if (collisionEmail)
        {
            ModelState.AddModelError("Email", "Email is already taken");
            return View(user);
        }
        
        
        
        
        User newUser = new User();

        var passwordHasher = new PasswordHasher<User>();
        var hashedpassword = passwordHasher.HashPassword(newUser, user.Password);
        

        newUser.Email = normalizedEmail;
        newUser.Username = normalizedUsername;
        newUser.PasswordHash = hashedpassword;

        await _context.Users.AddAsync(newUser);

        await _context.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    public async Task<IActionResult> SignOutAsync()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return Redirect("/Auth/Login");
    }
}