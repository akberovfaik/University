using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University.ViewModels;

namespace University.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>()
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            });
            base.OnModelCreating(builder);
        }
        public Microsoft.EntityFrameworkCore.DbSet<Grades> Grades { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Students> Students { get; set; }
    }
}
