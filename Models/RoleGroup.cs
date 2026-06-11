using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("Role_Group")]
    public class RoleGroup
    {
        [Key]
        [Column("Role_Group_ID")]
        public int Id { get; set; }

        [Column("Role_ID")]
        public int RoleId { get; set; }

        [Column("Group_ID")]
        public int GroupId { get; set; }

        public virtual required Role Role { get; set; }
        public virtual required Group Group { get; set; }
    }
}