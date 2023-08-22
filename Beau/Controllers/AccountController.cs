using Microsoft.AspNetCore.Mvc;
using Beau.Data;
using Beau.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Beau.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DataBContext dbcon;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController( DataBContext dbcont, IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            dbcon = dbcont;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }
        public IActionResult LoginView()
        {
            ViewBag.message = TempData["message"];
            return View();
        }
        public async Task<IActionResult> LogProcess(UserInfo model, UserCredentials creden)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsJsonAsync("https://localhost:7235/api/accountapi/authenticatelogs", model);

            if (response.IsSuccessStatusCode)
            {
                var userId = await response.Content.ReadAsAsync<Guid>();
                if (userId != null)
                {
                    var user = dbcon.Credentials.FirstOrDefault(u => u.UserId == userId);
                    if (user != null)
                    {
                        var uname = user.UserName;
                    }
                    return RedirectToAction("Index", "Home" );
                }
            }
            TempData["message"] = Convert.ToString(response);
            return RedirectToAction("LoginView");
        }
        public IActionResult SignUpView()
        {
            return View();
        }
        public async Task<IActionResult> SignUpProcess(UserInfo model, UserCredentials cred)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {

                    Email = cred.Email
                };
                var result = await _userManager.CreateAsync(user, cred.Password);

                if (result.Succeeded)
                {
                    var userProfile = new UserInfo
                    {
                        Fname = model.Fname,
                        Lname = model.Lname,
                        Phone = model.Phone,
                        birthday = model.birthday
                    };

                    dbcon.Add(userProfile);
                    await dbcon.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            TempData["Error"] = "Invalid ModelState";
            return View("SignUpView");

        }
    }
}
