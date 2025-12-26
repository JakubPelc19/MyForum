using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyForum.Controllers
{
    [Route("Thread/{threadId:int}/[controller]/{postId:int}")]
    public class PostController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int threadId, int postId)
        {
            
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.ThreadForumId == threadId && p.Id == postId);

            if (post is null)
                return NotFound();

            var comments = await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
            
            return View(comments);
        }
    }
}
