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
        public DbSet<CaseStudy> CaseStudies { get; set; }
        public DbSet<CaseStudyFigure> CaseStudyFigures { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<LeadershipStrategy> LeadershipStrategies { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CaseStudyFigure>(x => x.HasKey(p => new { p.FigureId, p.CaseStudyId }));

            modelBuilder.Entity<CaseStudyFigure>()
                .HasOne(p => p.Figure)
                .WithMany(p => p.CaseStudyFigures)
                .HasForeignKey(p => p.FigureId);

            modelBuilder.Entity<CaseStudyFigure>()
                .HasOne(p => p.CaseStudy)
                .WithMany(p => p.CaseStudyFigures)
                .HasForeignKey(p => p.CaseStudyId);


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