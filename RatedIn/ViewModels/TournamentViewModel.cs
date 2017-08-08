using RatedIn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RatedIn.ViewModels
{
    public class TournamentViewModel
    {
        public int TournamentId { get; set; }

        
        [DisplayName("Tournament Name")]
        public string TournamentName { get; set; }

        [DisplayName("Tournament Admin")]
        public string TournamentAdminId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<Attendance> Attendances { get; set; }
        public IEnumerable<Player> Players { get; set; } 
    }
}