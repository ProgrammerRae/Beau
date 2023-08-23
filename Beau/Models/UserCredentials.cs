using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class UserCredentials : IdentityUser
    {
 

        public UserInfo UserInfo { get; set; }
    }
}
