using Microsoft.AspNetCore.Mvc;
using Beau.Data;
using Beau.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Beau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<int>> GetUser(UserInfo model)
        {
            var user = await dbcon.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                TempData["message"] = "Sorry Info Not Found";
                return RedirectToAction("LoginView");
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);

            return RedirectToAction("Index", "Home");
        }


    }
}
