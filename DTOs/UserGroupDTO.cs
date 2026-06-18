using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_service.Models
{
    public record UserGroupDTO(Guid Id, UserDTO User, GroupDTO Group) {}
}