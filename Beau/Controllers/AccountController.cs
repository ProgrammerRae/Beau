using Microsoft.AspNetCore.Mvc;
using Beau.Data;
using Beau.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;
using NHibernate.Id.Insert;

namespace Beau.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DataBContext dbcon;
        public AccountController( DataBContext dbcont, IHttpClientFactory httpClientFactory)
        {
            dbcon = dbcont;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult LoginView()
        {
            ViewBag.message = TempData["message"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginProcess(string emailOrUsername, string passwordHash)
        {
            if(ModelState.IsValid)
            {
                var users = await dbcon.Credentials
                .Include(c => c.userInfo)
                .Where(u => u.Email == emailOrUsername || u.UserName == emailOrUsername)
                .ToListAsync();

                var user = users.FirstOrDefault(u => BCrypt.Net.BCrypt.Verify(passwordHash, u.PasswordHash));

                if (user != null)
                {

                    return RedirectToAction("Index", "Home", new { id = user.userInfo.UserId });
                }

                TempData["message"] = "Invalid credentials.";
                return RedirectToAction("LoginView");
            }
            TempData["message"] = "Invalid ModelState";
            return RedirectToAction("LoginView");
        }


        public IActionResult SignUpView()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpProcess(UserInfo model)
        {
            if (ModelState.IsValid) { 
                if (model != null)
                {
                    var user = new UserCredentials
                    {
                        UserName = model.UserCredentials.UserName,
                        Email = model.UserCredentials.Email,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.UserCredentials.PasswordHash)
                    };


                    var userProfile = new UserInfo
                    {
                        Fname = model.Fname,
                        Lname = model.Lname,
                        Phone = model.Phone,
                        birthday = model.birthday,
                        UserCredentials = user
                    };

                    dbcon.Add(user);
                    dbcon.Add(userProfile);
                    await dbcon.SaveChangesAsync();

                    return RedirectToAction("Index", "Home", new { id = userProfile.UserId });
                }

            return View("LoginView");
            }
            return  View("SignUpView", model);
        }
    }
}
