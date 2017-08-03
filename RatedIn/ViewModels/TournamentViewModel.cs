using RatedIn.Models;
using System;
using System.Collections.Generic;

namespace RatedIn.ViewModels
{
    public class TournamentViewModel
    {
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<Players> Players { get; set; }
    }
}