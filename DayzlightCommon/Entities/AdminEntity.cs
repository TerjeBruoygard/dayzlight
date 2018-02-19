using System;
using System.ComponentModel.DataAnnotations;

namespace DayzlightCommon.Entities
{
    public class AdminEntity
    {
        [Key]
        public Int64 Id { get; set; }

        [Required]
        public String Login { get; set; }

        [Required]
        public String Password { get; set; }
    }
}