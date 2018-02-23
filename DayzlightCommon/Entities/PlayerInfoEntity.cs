using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities
{
    public class PlayerInfoEntity
    {
        [Column(TypeName = "VARCHAR")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Index(IsUnique = true)]
        [Required]
        [StringLength(64)]
        public String Uid { set; get; }

        [Required]
        [StringLength(32)]
        public String Color { set; get; }

        public ICollection<PlayerNameEntity> Names { get; set; }
    }
}
