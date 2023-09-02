
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Status")]
        public string Status { get; set; }

        public Guid UserId { get; set; }
        public UserInfo UserInfo { get; set; }

    }
}
