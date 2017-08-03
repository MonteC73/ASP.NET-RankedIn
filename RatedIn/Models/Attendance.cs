using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatedIn.Models
{
    public class Attendance
    {
        public Tournament Tournament { get; set; }
        public Players Player { get; set; }

        [Key]
        [Column(Order = 1)]
        public int TournamentId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int PlayerId { get; set; }

        public int Score { get; set; }
    }
}