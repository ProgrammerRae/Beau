using Beau.Data;
using Beau.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Beau.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBContext dbcon;
        public HomeController(DataBContext dbcon)
        {
            this.dbcon = dbcon;
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? userInfo = HttpContext.Session.GetInt32("UserId");
            if(userInfo.HasValue)
            {
                var user = dbcon.Users.FirstOrDefault(u => u.UserId == userInfo);
                if (user != null)
                {
                    string username = user.Fname;
                    ViewBag.Username = username;
                }
               
            }
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}