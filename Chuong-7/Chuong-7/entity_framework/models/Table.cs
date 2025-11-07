using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("Table")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TableCode { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        public int Status { get; set; } = 0; // 0: chưa đặt, 1: đã đặt, 2: có khách

        public int? Seats { get; set; }

        [Required]
        public int HallID { get; set; }

        // Navigation properties
        [ForeignKey("HallID")]
        public virtual Hall Hall { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
