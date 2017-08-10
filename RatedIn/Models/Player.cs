

using System.Collections.Generic;

namespace RatedIn.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Games { get; set; }

        public ICollection<FilePath> FilePaths { get; set; }
    }
}