using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class UserCredentials
    {
        [Key]
        public Guid IdCred { get; set; }
        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string PasswordHash { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
        public UserInfo userInfo { get; set; }


    }
}
