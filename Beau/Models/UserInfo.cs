using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class UserInfo
    {
        private int age;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string Fname { get; set; } = string.Empty;
        [Required]
        public string Lname { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime birthday { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public List<Post>? Posts { get; set; }
        public int? PostId { get; set; }
        public int Age
        {
            get
            {
                DateTime birthdayValue = this.birthday;
                int Yr = birthdayValue.Year;
                int Mt = birthdayValue.Month;
                DateTime TodayDate = DateTime.Today;
                int TNow = TodayDate.Year;
                int MNow = TodayDate.Month;
                int result = Mt.CompareTo(MNow);

                if (result < 0)
                {
                    age = TNow - Yr - 1;
                }
                else if (result > 0)
                {
                    age = TNow - Yr;
                }
                
                return age;
            }
        }
    }
}
