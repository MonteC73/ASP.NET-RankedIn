

using System.ComponentModel.DataAnnotations.Schema;

namespace RatedIn.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FilePath
    {
        [Key]
        public int Id { get; set; }

        public Player Player { get; set; }
       
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public FileType FileType { get; set; }
    }
}