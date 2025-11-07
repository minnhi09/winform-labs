using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }

        [MaxLength(500)]
        public string? Path { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        // Navigation property
        public virtual ICollection<RoleAccount> RoleAccounts { get; set; } = new List<RoleAccount>();
    }
}
