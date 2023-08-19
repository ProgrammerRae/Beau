using Microsoft.AspNetCore.Mvc;
using Beau.Data;
using Beau.Models;
using Microsoft.EntityFrameworkCore;

namespace Beau.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DataBContext dbcon;
        public LoginController( DataBContext dbcont, IHttpClientFactory httpClientFactory)
        {
            dbcon = dbcont;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult LoginView()
        {
            ViewBag.message = TempData["message"];
            return View();
        }
        public async Task<IActionResult> LogProcess(UserInfo model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsJsonAsync("https://localhost:7235/api/fetchdata/getuser", model);

            if (response.IsSuccessStatusCode)
            {
                var userId = await response.Content.ReadAsAsync<int>();
                if (userId != 0)
                {
                    var user = dbcon.Users.FirstOrDefault(u => u.UserId == userId);
                    if (user != null)
                    {
                        string username = user.Fname;
                        ViewBag.Username = username;
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["message"] = Convert.ToString(response);
            return RedirectToAction("LoginView");
        }
    }
}
