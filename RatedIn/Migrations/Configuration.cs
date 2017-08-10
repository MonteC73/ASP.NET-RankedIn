using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RatedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RatedIn.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RatedIn.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RatedIn.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var passwordHash = new PasswordHasher();

            var players = new List<Player>
            {
                new Player {Id = 1, Name = "Player_1", Games = 0, Rating = 1200},
                new Player {Id = 2, Name = "Player_2", Games = 0, Rating = 1200},
                new Player {Id = 3, Name = "Player_3", Games = 0, Rating = 1200},
                new Player {Id = 4, Name = "Player_4", Games = 0, Rating = 1200},
                new Player {Id = 5, Name = "Player_5", Games = 0, Rating = 1200},
                new Player {Id = 6, Name = "Player_6", Games = 0, Rating = 1200},
                new Player {Id = 7, Name = "Player_7", Games = 0, Rating = 1200},
                new Player {Id = 8, Name = "Player_8", Games = 0, Rating = 1200}
            };

            var admin = new ApplicationUser { UserName = "Admin_1", Email = "admin@admin.net", PasswordHash = passwordHash.HashPassword("pass") };

            userManager.Create(admin);
            var adminId = userManager.Users.First(u => u.UserName.Equals("Admin_1")).Id;

            var filePaths = new List<FilePath>
            {
                new FilePath {FileName = "red.bmp", PlayerId = 1, FileType = FileType.Avatar},
                new FilePath {FileName = "black.bmp", PlayerId = 2, FileType = FileType.Avatar},
                new FilePath {FileName = "blue.bmp", PlayerId = 3, FileType = FileType.Avatar},
                new FilePath {FileName = "gray.bmp", PlayerId = 4, FileType = FileType.Avatar},
                new FilePath {FileName = "green.bmp", PlayerId = 5, FileType = FileType.Avatar},
                new FilePath {FileName = "navy.bmp", PlayerId = 6, FileType = FileType.Avatar},
                new FilePath {FileName = "orange.bmp", PlayerId = 7, FileType = FileType.Avatar},
                new FilePath {FileName = "purple.bmp", PlayerId = 8}
            };

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

            var attendances = new List<Attendance>
            {
                new Attendance
                {
                    PlayerId = 1,
                    TournamentId = 1
                },
                new Attendance
                {
                    PlayerId = 2,
                    TournamentId = 1
                },
                new Attendance
                {
                    PlayerId = 3,
                    TournamentId = 1
                },
                new Attendance
                {
                    PlayerId = 4,
                    TournamentId = 1
                },
                new Attendance
                {
                    PlayerId = 3,
                    TournamentId = 2
                },
                new Attendance
                {
                    PlayerId = 4,
                    TournamentId = 2
                },
                new Attendance
                {
                    PlayerId = 5,
                    TournamentId = 2
                },
                new Attendance
                {
                    PlayerId = 6,
                    TournamentId = 2
                                },
                new Attendance
                {
                    PlayerId = 5,
                    TournamentId = 3
                },
                new Attendance
                {
                    PlayerId = 6,
                    TournamentId = 3
                },
                new Attendance
                {
                    PlayerId = 7,
                    TournamentId = 3
                },
                new Attendance
                {
                    PlayerId = 8,
                    TournamentId = 3
                }
            };

            players.ForEach(p => context.Players.Add(p));
            filePaths.ForEach(f => context.FilePaths.Add(f));
            tournaments.ForEach(t => context.Tournaments.Add(t));
            attendances.ForEach(a => context.Attendances.Add(a));

            context.SaveChanges();
        }
    }
}
