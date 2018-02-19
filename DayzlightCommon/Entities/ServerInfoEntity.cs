using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities {
    public class ServerInfoEntity
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public Int64 Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public String WorldName { get; set; }

        [Required]
        public Double MinCornerX { get; set; }

        [Required]
        public Double MinCornerY { get; set; }

        [Required]
        public Double MaxCornerX { get; set; }

        [Required]
        public Double MaxCornerY { get; set; }

        [Required]
        public Double SpawnPointX { get; set; }

        [Required]
        public Double SpawnPointY { get; set; }
    }
}