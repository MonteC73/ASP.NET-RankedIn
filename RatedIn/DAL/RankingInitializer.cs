using RatedIn.Models;
using System.Collections.Generic;

namespace RatedIn.DAL
{
    public class RankingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RankingContext>
    {
        protected override void Seed(RankingContext context)
        {
            var players = new List<Player>
            {
                new Player {Id = 1, Name = "Player_1", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 2, Name = "Player_2", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 3, Name = "Player_3", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 4, Name = "Player_4", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 5, Name = "Player_5", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 6, Name = "Player_6", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 7, Name = "Player_7", Games = 0, Rating = 1200, URL = ""},
                new Player {Id = 8, Name = "Player_8", Games = 0, Rating = 1200, URL = ""}
            };

            players.ForEach(p => context.Players.Add(p));
            context.SaveChanges();
        }
    }
}