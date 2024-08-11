using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Figure> Figures{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles =
            [
                new(){
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new(){
                    Name = "User",
                    NormalizedName = "USER",
                },
            ];

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}