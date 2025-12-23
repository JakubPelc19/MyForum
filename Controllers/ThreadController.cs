using Microsoft.AspNetCore.Mvc;

namespace MyForum.Controllers
{
    public class ThreadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
