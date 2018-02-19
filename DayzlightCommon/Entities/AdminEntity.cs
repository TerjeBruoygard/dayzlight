using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayzlightCommon.Entities
{
    public class AdminEntity
    {
        [Key]
        [Index(IsUnique = true)]
        [Required]
        public Int64 Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(255)]
        [Display(Name = "Login")]
        public String Login { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}