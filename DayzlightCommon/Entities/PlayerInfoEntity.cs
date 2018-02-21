using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities
{
    public class PlayerInfoEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Index(IsUnique = true)]
        [Required]
        public Int64 Uid { set; get; }

        [Required]
        [StringLength(6)]
        public String Color { set; get; }

        public ICollection<PlayerNameEntity> Names { get; set; }
    }
}
