using Microsoft.AspNetCore.Mvc;

namespace Beau.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
