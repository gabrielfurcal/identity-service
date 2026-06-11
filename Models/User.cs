
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("User_ID")]
        public Guid Id { get; set; }

        [StringLength(255)]
        public required string Email { get; set; }

        [StringLength(128)]
        [Column("Password_Hash")]
        public required string PasswordHash { get; set; }

        [Column("Is_Active")]
        public bool IsActive { get; set; }

        [Column("Created_At")]
        public DateTime CreatedAt { get; set; }

        [Column("Updated_At")]
        public DateTime UpdatedAt { get; set; }

        public virtual HashSet<RefreshToken>? RefreshTokens { get; set; }
        public virtual HashSet<UserRole>? UserRoles { get; set; }
        public virtual HashSet<UserGroup>? UserGroups { get; set; }
    }
}