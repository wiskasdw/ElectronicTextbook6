using ElectronicTextbook.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicTextbook.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, string> // Верно
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Lecture> Lectures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);  //  Используйте RoleId

            builder.Entity<Lecture>()
                .HasOne(l => l.Author)
                .WithMany(u => u.Lectures)
                .HasForeignKey(l => l.AuthorId);
        }
    }
}