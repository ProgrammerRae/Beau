using Beau.Models;
using Microsoft.EntityFrameworkCore;

namespace Beau.Data
{
    public class DataBContext : DbContext
    {
        public DataBContext (DbContextOptions<DataBContext> options) : base(options) 
        {
        
        }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserCredentials> Credentials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserCredentials>()
                .HasOne(uc => uc.userInfo)
                .WithOne(ui => ui.UserCredentials)
                .HasForeignKey<UserInfo>(ui => ui.IdCred)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            modelBuilder.Entity<UserCredentials>()
                .HasIndex(eml => eml.Email)
                .IsUnique();
            modelBuilder.Entity<UserCredentials>()
               .HasIndex(eml => eml.UserName)
               .IsUnique();
            modelBuilder.Entity<Post>()
                .HasOne(us=> us.UserInfo)
                .WithMany(p => p.Posts)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
