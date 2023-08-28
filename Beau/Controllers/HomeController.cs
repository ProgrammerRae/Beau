using Beau.Data;
using Beau.Models;
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

        public IActionResult Index([FromQuery] Guid id)
        {
            var user = dbcon.Users
                .Include( inc => inc.UserCredentials)
                .FirstOrDefault(u => u.UserId == id);
            var uname = user.UserCredentials.UserName;
            TempData["iun"] = uname;
            ViewBag.name = TempData["iun"];
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

    }
}