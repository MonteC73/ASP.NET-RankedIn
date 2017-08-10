using RatedIn.Models;
using System.Collections.Generic;

namespace RatedIn.ViewModels
{
    public class GameViewModel
    {
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public List<Player> Players { get; set; }
        public ICollection<FilePath> FilePaths { get; set; } 
    }
}