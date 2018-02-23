using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities
{
    public class LivemapSettingsEntity
    {
        [Key]
        [Index(IsUnique = true)]
        [Required]
        public Int64 Id { get; set; }

        [Required]
        public Boolean MenuExpanded { get; set; }

        [Required]
        public Int16 PlayersPathLength { get; set; }

        [Required]
        public Boolean HideSpawnTp { get; set; }

        [Required]
        public Boolean ClearPathAfterDisconnect { get; set; }

        [Required]
        [StringLength(8)]
        public String OverlayIconsColor { get; set; }

        [Required]
        public Double OverlayIconsSize { get; set; }

        [Required]
        public Boolean TimelineExpanded { get; set; }
    }
}
