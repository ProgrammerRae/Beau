using Beau.Data;
using Beau.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        [Route("AuthenticateLogs")]
        public async Task<ActionResult<int>> AuthenticateLogs(UserCredentials model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                return BadRequest("Invalid request data.");
            }

            var user = await dbcon.Credentials
                .Include(x => x.UserInfo)
                .FirstOrDefaultAsync(u => u.Email == model.Email || u.Email == model.UserName && u.PasswordHash == model.PasswordHash);
           
            if (user == null)
            {
                return Unauthorized();
            }
            var userInfoUserId = user.UserInfo.UserId;
            _logger.LogInformation("INFO" + Convert.ToString(userInfoUserId));
            return Ok(userInfoUserId);
        }
    }
}
