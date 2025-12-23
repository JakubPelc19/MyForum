using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyForum.Views.Thread;

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

            var posts = thread.Posts;

            
            ViewData["ThreadTitle"] = thread.Title;
            return View(posts);
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostModel model)
        { 
            if (!ModelState.IsValid)
            {

            }
        }
    }
}
