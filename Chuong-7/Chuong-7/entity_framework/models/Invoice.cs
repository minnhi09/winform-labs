using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "Hóa đơn chưa thanh toán";

        [Required]
        public int TableID { get; set; }

        [Required]
        public int Total { get; set; } = 0;

        public double? Discount { get; set; } = 0;

        public double? Tax { get; set; } = 0;

        [Required]
        public bool Status { get; set; } = false; // 0: chưa thanh toán, 1: đã thanh toán

        [Required]
        [MaxLength(50)]
        public string AccountID { get; set; } = "admin";

        [Required]
        public DateTime CheckoutDate { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("TableID")]
        public virtual Table Table { get; set; }

        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; } = new List<InvoiceDetails>();
    }
}
