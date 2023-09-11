using Microsoft.AspNetCore.Mvc;
using Beau.Data;
using Beau.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
                   
                    HttpContext.Session.SetString("UserId", user.userInfo.UserId.ToString());
                    return RedirectToAction("Index", "Home");
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
            var emailExist = dbcon.Credentials.Where(ems => ems.Email == model.UserCredentials.Email);
            if (emailExist!= null)
            {
                ModelState.AddModelError("Email", " Email Already Exist");
            }
            var userNameExist = dbcon.Credentials.Where(usr => usr.UserName == model.UserCredentials.UserName);
            if (userNameExist != null)
            {
                ModelState.AddModelError("Username", "Username Already Exist");
            }
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

                    HttpContext.Session.SetString("UserId", model.UserId.ToString());
                    return RedirectToAction("Index", "Home");
                }

                return View("LoginView");
            }
            return  View("SignUpView", model);
        }
    }
}
