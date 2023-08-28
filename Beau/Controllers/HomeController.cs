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

        public IActionResult Index(Guid id)
        {
            var user = dbcon.Users
                .Include( inc => inc.UserCredentials)
                .FirstOrDefault(u => u.UserId == id);

            if (user != null)
            {
                var fkID = user.UserCredentials.IdCred;
                var userCred = dbcon.Credentials.FirstOrDefault(pk => pk.IdCred == fkID);

                if (userCred != null)
                {
                    ViewBag.name = userCred.UserName;
                }
                else
                {
                    ViewBag.name = "null";
                }
            }
            else
            {
                ViewBag.name = "User not found"; 
            }

            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

    }
}