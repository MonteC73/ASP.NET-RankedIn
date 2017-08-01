﻿
namespace RatedIn.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FilePath
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int PlayerId { get; set; }
    }
}