using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [Column("Role_ID")]
        public int Id { get; set; }

        [StringLength(60)]
        public required string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}