using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RatedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RatedIn.DAL
{
    public class RatedInInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var passwordHash = new PasswordHasher();

            var players = new List<Players>
            {
                new Players {Id = 1, Name = "Player_1", Games = 0, Rating = 1200},
                new Players {Id = 2, Name = "Player_2", Games = 0, Rating = 1200},
                new Players {Id = 3, Name = "Player_3", Games = 0, Rating = 1200},
                new Players {Id = 4, Name = "Player_4", Games = 0, Rating = 1200},
                new Players {Id = 5, Name = "Player_5", Games = 0, Rating = 1200},
                new Players {Id = 6, Name = "Player_6", Games = 0, Rating = 1200},
                new Players {Id = 7, Name = "Player_7", Games = 0, Rating = 1200},
                new Players {Id = 8, Name = "Player_8", Games = 0, Rating = 1200}
            };

            var admin = new ApplicationUser{UserName = "Admin_1", Email = "admin@admin.net", PasswordHash = passwordHash.HashPassword("pass")};

            userManager.Create(admin);
            var adminId = userManager.Users.First(u => u.UserName.Equals("Admin_1")).Id;

            var tournaments = new List<Tournament>
            {
                new Tournament
                {
                    Id = 1,
                    Name = "Tournament_1",
                    StartDate = new DateTime(2017, 8, 1),
                    EndDate = new DateTime(2017, 8, 14),
                    AdminId = adminId
                },
                new Tournament
                {
                    Id = 2,
                    Name = "Tournament_2",
                    StartDate = new DateTime(2017, 8, 15),
                    EndDate = new DateTime(2017, 8, 31),
                    AdminId = adminId
                },
                new Tournament
                {
                    Id = 3,
                    Name = "Tournament_3",
                    StartDate = new DateTime(2017, 8, 1),
                    EndDate = new DateTime(2017, 8, 31),
                    AdminId = adminId
                }
            };

            players.ForEach(p => context.Players.Add(p));
            tournaments.ForEach(t => context.Tournaments.Add(t));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}