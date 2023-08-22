using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [Required]
        public string Fname { get; set; } = string.Empty;
        [Required]
        public string Lname { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime birthday { get; set; }

        public ICollection<Post> Posts { get; set; }
        [NotMapped]
        public int Age => DateComputer.CalculateAge(birthday);
        [ForeignKey("UserCredentials")]
        public Guid IdCred { get; set; }
        public UserCredentials Credentials { get; set; }
    }
}
