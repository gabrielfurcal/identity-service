using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("User_Role")]
    public class UserRole
    {
        [Key]
        [Column("User_Role_ID")]
        public Guid Id { get; set; }

        [Column("User_ID")]
        public Guid UserId { get; set; }

        [Column("Role_ID")]
        public int RoleId { get; set; }

        public virtual required User User { get; set; }
        public virtual required Role Role { get; set; }
    }
}