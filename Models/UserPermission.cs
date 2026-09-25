using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("User_Permission")]
    public class UserPermission
    {
        [Key]
        [Column("User_Permission_ID")]
        public Guid Id { get; set; }

        [Column("User_ID")]
        public Guid UserId { get; set; }

        [Column("Permission_ID")]
        public int PermissionId { get; set; }

        public virtual required User User { get; set; }
        public virtual required Permission Permission { get; set; }
    }
}