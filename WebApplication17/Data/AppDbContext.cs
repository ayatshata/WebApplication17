using Microsoft.EntityFrameworkCore;
using WebApplication17.Models;

namespace WebApplication17.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Ayat Ali", Email = "yuy@example.com", CreatedAt = DateTime.Now },
                new User { Id = 2, Name = "Ali Ahmed", Email = "ali@example.com", CreatedAt = DateTime.Now },
                               new User { Id = 3, Name = "Ahmed Ali", Email = "ahe@example.com", CreatedAt = DateTime.Now }


            );
        }
    }
}