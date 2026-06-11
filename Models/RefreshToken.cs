using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("Refresh_Tokens")]
    public class RefreshToken
    {
        [Key]
        [Column("Ref_Tk_ID")]
        public Guid Id { get; set; }

        [StringLength(128)]
        [Column("Token_Hash")]
        public required string TokenHash { get; set; }

        [Column("Created_At")]
        public DateTime CreatedAt { get; set; }

        [Column("Expires_At")]
        public DateTime ExpiresAt { get; set; }

        [Column("Revoked_At")]
        public DateTime? RevokedAt { get; set; }

        [Column("Replaced_By_Token_ID")]
        public Guid? ReplacedByTokenId { get; set; }

        [StringLength(60)]
        [Column("Device_Info")]
        public string? DeviceInfo { get; set; }

        [StringLength(45)]
        [Column("IP_Address")]
        public string? IPAddress { get; set; }

        [Column("User_ID")]
        public required string UserId { get; set; }

        public virtual required User User { get; set; }
    }
}