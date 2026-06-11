using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        [Column("Group_ID")]
        public int Id { get; set; }

        [StringLength(60)]
        public required string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}