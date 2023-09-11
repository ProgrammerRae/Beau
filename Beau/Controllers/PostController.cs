using Beau.Data;
using Beau.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beau.Controllers
{
    public class PostController : Controller
    {
        private readonly DataBContext _dataBContext;
        public PostController(DataBContext dbcont)
        {
            this._dataBContext = dbcont;
        }
        
        public IActionResult CreatePostView()
        {
            
            return View();
        }
        public async Task<IActionResult> PostSomething(Post posted)
        {
            string idIgot = HttpContext.Session.GetString("UserId");
            Guid id = Guid.Parse(idIgot);

            var post = new Post
            {
                UserId = id,
                Status = posted.Status,
                PostDate = DateTime.UtcNow

            };
            _dataBContext.Add(post);    
            await _dataBContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var post = _dataBContext.Posts.Where(m => m.PostId == id).FirstOrDefault();
            return View(post);
        }
    }
}
