using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities
{
    public class PlayerNameEntity
    {
        [Key]
        [Index(IsUnique = true)]
        [Required]
        public Int64 Id { get; set; }

        [Index(IsUnique = false)]
        [Required]
        public PlayerInfoEntity PlayerInfo { set; get; }

        [Index(IsUnique = false)]
        [Required]
        [StringLength(255)]
        public String Name { get; set; }
    }
}
