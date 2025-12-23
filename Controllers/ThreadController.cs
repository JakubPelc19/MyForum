using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyForum.Controllers
{
    [Route("[controller]")]
    public class ThreadController(AppDbContext _context) : Controller
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            var thread = await _context.Threads.FindAsync(id);

            if (thread is null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.Where(p => p.ThreadId == id).ToListAsync();

            
            ViewData["ThreadTitle"] = thread.Title;
            return View(posts);
        }
    }
}
