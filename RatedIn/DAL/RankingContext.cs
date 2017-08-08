using RatedIn.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RatedIn.DAL
{
    public class RankingContext : IdentityDbContext<ApplicationUser>
    {
        public RankingContext() : base("RankingContext")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}