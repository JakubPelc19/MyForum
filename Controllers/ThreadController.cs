using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyForum.Views.Thread;
using System.Security.Claims;

namespace MyForum.Controllers
{
    [Route("[controller]/{id:int}")]
    public class ThreadController(AppDbContext _context) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(IndexModel model, int id)
        {
            var thread = await _context.Threads.FindAsync(id);

            if (thread is null)
            {
                return NotFound();
            }

            model.ThreadId = id;
            model.Posts = await _context.Posts.Where(p => p.ThreadForumId == id).OrderBy(p => p.Id).ToListAsync();

            


            ViewData["ThreadTitle"] = thread.Title;
            return View(model);
        }

        [Authorize]
        [HttpGet("CreatePost")]
        public async Task<IActionResult> CreatePost(int id)
        {
            var thread = await _context.Threads.FindAsync(id);

            if (thread is null)
            {
                return NotFound();
            }

            return View();
        }

        
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreatePostModel model, int id)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var thread = await _context.Threads.FindAsync(id);

            if (thread is null)
            {
                return NotFound();
            }

            var title = model.Title.Trim();
            var content = model.Content.Trim();
            var authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (authorId is null)
            {
                return Unauthorized();
            }

            if (string.IsNullOrEmpty(title))
            {
                ModelState.AddModelError("Title", "Title cannot be empty.");
                return View(model);
            }

            if (string.IsNullOrEmpty(content))
            {
                ModelState.AddModelError("Content", "Content cannot be empty.");
                return View(model);
            }

            var post = new Post
            {
                Title = title,
                Content = content,
                AuthorId = authorId,
                ThreadForumId = thread.Id
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = post.ThreadForumId });
        }

        [Authorize]
        [HttpGet("EditThread")]
        public async Task<IActionResult> EditThread(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            
            if (thread is null)
                return NotFound();

            return View();

        }

        [HttpPost("EditThread")]
        public async Task<IActionResult> EditThread(EditThreadModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var thread = await _context.Threads.FindAsync(id);

            if (thread is null)
                return NotFound();

            var title = model.Title.Trim();
            var description = model.Description?.Trim();

            if (string.IsNullOrEmpty(title))
            {
                ModelState.AddModelError("Title", "Title cannot be empty.");
                return View(model);
            }

            thread.Title = title;
            thread.Description = description;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = thread.Id });


        }
        

    }
}
