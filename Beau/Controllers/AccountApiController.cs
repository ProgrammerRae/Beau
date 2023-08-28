using Beau.Data;
using Beau.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BCrypt.Net;

namespace Beau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataBContext dbcon;
        private readonly ILogger<AccountApiController> _logger;

        public AccountApiController(DataBContext dbcon, ILogger<AccountApiController> logger, UserManager<IdentityUser> userManager)
        {
            this.dbcon = dbcon;
            _logger = logger;
            _userManager = userManager;
        }

        
    }
}
