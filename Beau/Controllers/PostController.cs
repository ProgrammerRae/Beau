using Beau.Data;
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
        public IActionResult Index()
        {
            return View();
        }

    }
}
