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
        
        public IActionResult CreatePostView(Guid id)
        {
            
            return View();
        }
        public async Task<IActionResult> PostSomething(Post posted)
        {

            var post = new Post
            {
                Status = posted.Status,
                PostDate = DateTime.UtcNow

            };
            _dataBContext.Add(post);    
            await _dataBContext.SaveChangesAsync();
            return RedirectToAction("Home", "Index");
        }

    }
}
