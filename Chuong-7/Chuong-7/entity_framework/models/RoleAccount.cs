using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("RoleAccount")]
    public class RoleAccount
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(50)]
        public string AccountName { get; set; }

        [Key]
        [Column(Order = 1)]
        public int RoleID { get; set; }

        [Required]
        public bool Actived { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }

        // Navigation properties
        [ForeignKey("AccountName")]
        public virtual Account Account { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}
