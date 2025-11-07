using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("Hall")]
    public class Hall
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "Chưa đặt tên";

        [Required]
        public int RestaurantID { get; set; }

        // Navigation properties
        [ForeignKey("RestaurantID")]
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}
