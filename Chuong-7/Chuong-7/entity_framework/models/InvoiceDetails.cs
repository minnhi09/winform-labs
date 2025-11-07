using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("InvoiceDetails")]
    public class InvoiceDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int InvoiceID { get; set; }

        [Required]
        public int FoodID { get; set; }

        [Required]
        public int Price { get; set; } = 0;

        [Required]
        public int Amount { get; set; } = 0;

        // Navigation properties
        [ForeignKey("InvoiceID")]
        public virtual Invoice Invoice { get; set; }

        [ForeignKey("FoodID")]
        public virtual Food Food { get; set; }
    }
}
