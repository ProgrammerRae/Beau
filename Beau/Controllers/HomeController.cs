using Beau.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beau.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBContext dbcon;
        public HomeController(DataBContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

    }
}