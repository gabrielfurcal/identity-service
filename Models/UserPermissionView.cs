using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("User_Permissions")]
    public class UserPermissionView
    {
        public Guid UserId { get; set; }

        public required string PermissionName { get; set; }
    }
}