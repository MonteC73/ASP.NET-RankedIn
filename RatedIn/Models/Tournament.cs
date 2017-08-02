using System;
using System.Collections.Generic;

namespace RatedIn.Models
{
    public class Tournament
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Players> Players { get; set; }
    }
}