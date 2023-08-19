using Beau.Data;
using Beau.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FetchDataController : ControllerBase
    {
        private readonly DataBContext dbcon;

        public FetchDataController()
        {
        }

        public FetchDataController(DataBContext dbcon)
        {
            this.dbcon = dbcon;
        }

        [HttpPost]
        [Route("GetUser")]
        public async Task<ActionResult<int>> GetUser(UserInfo model)
        {
            try
            {
                var user = await dbcon.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user.UserId);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while processing your request.");
            }
        }
    }
}
