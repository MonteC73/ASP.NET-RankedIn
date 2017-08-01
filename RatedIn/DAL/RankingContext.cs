using RatedIn.Models;
using System.Data.Entity;

namespace RatedIn.DAL
{
    public class RankingContext : DbContext
    {
        public RankingContext() : base("RankingContext")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
    }
}