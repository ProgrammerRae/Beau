using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class UserInfo
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [DisplayName("First Name")]
        public string Fname { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [DisplayName("Last Name")]
        public string Lname { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\+?[\d\s]*$", ErrorMessage = "Invalid phone number format.")]
        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Birthday")]
        public DateTime birthday { get; set; }

        public ICollection<Post> Posts { get; set; }

        [NotMapped]
        public int Age => DateComputer.CalculateAge(birthday);

        
        [ForeignKey("UserCredentials")]
        public Guid IdCred { get; set; }
        public UserCredentials UserCredentials { get; set; }
    }
}
