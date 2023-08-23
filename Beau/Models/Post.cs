

using FluentNHibernate.Conventions.Inspections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beau.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        [ForeignKey("UserInfo")]
        public int UserId { get; set; }
        public UserInfo UserInfo { get; set; }
        public DateTime PostDate{ get; set;  }
        [Required]
        public string Status { get; set; }

    }
}
