using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("User_Roles")]
    public class UserRoleView
    {
        public Guid UserId { get; set; }

        public required string RoleName { get; set; }
    }
}