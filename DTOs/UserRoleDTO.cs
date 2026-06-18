namespace identity_service.Models
{
    public record UserRoleDTO(Guid Id, UserDTO User, RoleDTO Role)
    {}
}