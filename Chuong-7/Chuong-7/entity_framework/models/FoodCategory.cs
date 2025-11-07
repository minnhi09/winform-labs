using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("FoodCategory")]
    public class FoodCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "Chưa đặt tên";

        [Required]
        public int Type { get; set; } // 1: đồ ăn, 2: thức uống

        // Navigation property
        public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}
