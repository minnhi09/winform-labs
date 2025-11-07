using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("Food")]
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "Chưa đặt tên";

        [Required]
        [MaxLength(50)]
        public string Unit { get; set; }

        [Required]
        public int FoodCategoryID { get; set; }

        [Required]
        public int Price { get; set; } = 0;

        [MaxLength(500)]
        public string? Notes { get; set; }

        // Navigation properties
        [ForeignKey("FoodCategoryID")]
        public virtual FoodCategory FoodCategory { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; } = new List<InvoiceDetails>();
    }
}
