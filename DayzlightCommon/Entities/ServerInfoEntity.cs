using System;
using System.ComponentModel.DataAnnotations;

namespace DayzlightCommon.Entities {
    public class ServerInfoEntity
    {
        [Key]
		public Int64 Id { get; set; }

        [Required]
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