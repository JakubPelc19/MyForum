using Microsoft.AspNetCore.Mvc;

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

            var comments = post.Comments;
            
            return View(comments);
        }
    }
}
