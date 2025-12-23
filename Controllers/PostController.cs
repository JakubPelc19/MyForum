using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyForum.Controllers
{
    [Route("Thread/{threadId:int}/[controller]")]
    public class PostController(AppDbContext _context) : Controller
    {

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post is null)
                return NotFound();

            var comments = await _context.Comments.Where(c => c.PostId == id).ToListAsync();
            
            return View(comments);
        }
    }
}
