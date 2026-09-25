using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("Role_Permission")]
    public class RolePermission
    {
        [Key]
        [Column("Role_Permission_ID")]
        public int Id { get; set; }

        [Column("Role_ID")]
        public int RoleId { get; set; }

        [Column("Permission_ID")]
        public int PermissionId { get; set; }

        public virtual required Role Role { get; set; }
        public virtual required Permission Permission { get; set; }
    }
}