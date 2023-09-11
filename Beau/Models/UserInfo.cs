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

        [Required(ErrorMessage = "Please enter your First Name")]
        [DataType(DataType.Text)]
        [StringLength(100,ErrorMessage = "Name is Too long")]
        [DisplayName("First Name")]
        public string Fname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your Last Name")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Name is Too long")]
        [DisplayName("Last Name")]
        public string Lname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your Phone Number")]
        [RegularExpression(@"^(\+[0-9\s]*|0[0-9\s]*)$", ErrorMessage = "Invalid phone number format.")]
        [Phone(ErrorMessage = "Invalid Characters")]
        [DisplayName("Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your Birthday")]
        [DataType(DataType.Date)]
        [DisplayName("Birthday")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthday { get; set; }

        public ICollection<Post>? Posts { get; set; }

        [NotMapped]
        public int Age => DateComputer.CalculateAge(birthday);

        
        [ForeignKey("UserCredentials")]
        public Guid IdCred { get; set; }
        public UserCredentials UserCredentials { get; set; }
    }
}
