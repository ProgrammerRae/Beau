using Microsoft.AspNetCore.Mvc;
using Beau.Data;
using Beau.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Beau.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataBContext dbcon;
        public LoginController( DataBContext dbcont)
        {
            dbcon = dbcont;
        }
        public IActionResult LoginView()
        {
            ViewBag.message = TempData["message"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogProcess(UserInfo model)
        {
            var user = await dbcon.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                TempData["message"] = "Error Info";
                return View("LoginView");
            }

            return View("../Home/Index");
        }
    }
}
