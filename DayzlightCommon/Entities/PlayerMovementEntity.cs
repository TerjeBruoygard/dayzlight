using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities
{
    public class PlayerMovementEntity
    {
        [Key]
        [Index(IsUnique = true)]
        [Required]
        public Int64 Id { set; get; }

        [Index(IsUnique = false)]
        [Required]
        public TimepointEntity Timepoint { set; get; }

        [Index(IsUnique = false)]
        [Required]
        public PlayerNameEntity PlayerName { set; get; }
        
        [Required]
        public Double PosX { set; get; }

        [Required]
        public Double PosY { set; get; }

        [Required]
        public Double Dir { set; get; }
    }
}
