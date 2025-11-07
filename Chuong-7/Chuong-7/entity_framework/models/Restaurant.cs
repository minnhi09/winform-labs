using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("Restaurant")]
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "Chưa đặt tên";

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = "Chưa có địa chỉ";

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = "02633...";

        [MaxLength(200)]
        public string? Website { get; set; }

        // Navigation property
        public virtual ICollection<Hall> Halls { get; set; } = new List<Hall>();
    }
}
