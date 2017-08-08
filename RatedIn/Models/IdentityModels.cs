using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RatedIn.DAL;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RatedIn.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new RatedInInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Tournament>()
        //    //    .HasOptional(p => p.Player)
        //    //    .WithMany()
        //    //    .WillCascadeOnDelete(false);

        //    //modelBuilder.Entity<Attendance>()
        //    //    .HasRequired(t => t.Tournament)
        //    //    .WithMany()
        //    //    .WillCascadeOnDelete(false);

        //    //base.OnModelCreating(modelBuilder);
        //}

    }
}