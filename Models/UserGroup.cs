using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    [Table("User_Group")]
    public class UserGroup
    {
        [Key]
        [Column("User_Group_ID")]
        public Guid Id { get; set; }

        [Column("User_ID")]
        public Guid UserId { get; set; }

        [Column("Group_ID")]
        public int GroupId { get; set; }

        public virtual required User User { get; set; }
        public virtual required Group Group { get; set; }
    }
}