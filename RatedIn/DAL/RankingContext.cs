using RatedIn.Models;
using System.Data.Entity;

namespace RatedIn.DAL
{
    public class RankingContext : DbContext
    {
        public RankingContext() : base("RankingContext")
        {
        }

        public DbSet<Players> Players { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}