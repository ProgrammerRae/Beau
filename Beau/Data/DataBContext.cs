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
            modelBuilder.Entity<UserInfo>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.UserInfo)
                .HasForeignKey(fk => fk.UserId);
            modelBuilder.Entity<UserCredentials>()
                .HasIndex(eml => eml.Email)
                .IsUnique();
            modelBuilder.Entity<UserCredentials>()
               .HasIndex(eml => eml.UserName)
               .IsUnique();
            modelBuilder.Entity<UserInfo>()
                .HasOne<UserCredentials>()
                .WithOne(uc=> uc.UserInfo)
                .HasForeignKey<UserInfo>(ucred => ucred.UserId);
        }

    }
}
