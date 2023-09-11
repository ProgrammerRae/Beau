using Beau.Data;
using Beau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Beau.Repository;

namespace Beau.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBContext dbcon;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        public HomeController(DataBContext dbcon, PostRepository postRepository, UserRepository userRepository)
        {
            this.dbcon = dbcon;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            string idIgot = HttpContext.Session.GetString("UserId");
            Guid id = Guid.Parse(idIgot);

            var userLogName = _userRepository.GetUserNameByID(id);

            var userInfo = new UserInfo();
            userInfo.Posts = _postRepository.GetPostsByUserId(id);


            if (userInfo.Posts != null)
            {
                
                ViewBag.name = userLogName;
                return View(userInfo);
            }

            return View();
        }

        public IActionResult Profile()
        {
            string idIgot = HttpContext.Session.GetString("UserId");
            Guid id = Guid.Parse(idIgot);

            return View();
        }

    }
}