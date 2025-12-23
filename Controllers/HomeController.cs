using System.Collections.Immutable;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyForum.Views.Home;

namespace MyForum.Controllers;

public class HomeController(AppDbContext _context) : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var threads = await _context.Threads.ToListAsync();
        return View(threads);
    }
    
    [Authorize]
    public IActionResult CreateThread()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateThread(CreateThreadModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var title = model.Title.Trim();
        var description = model.Description?.Trim();
        var authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(title))
        {
            ModelState.AddModelError("Title", "The Title field is required.");
            return View(model);
        }
        
        if (authorId is null)
        {
            return Unauthorized();
        }
        
        ThreadForum thread = new ThreadForum
        {
            Title = title,
            Description = description,
            AuthorId = authorId
        };

        await _context.Threads.AddAsync(thread);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
