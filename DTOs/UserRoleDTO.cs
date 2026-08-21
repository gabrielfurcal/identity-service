namespace identity_service.DTOs
{
    public record UserRoleDTO(Guid Id, UserDTO User, RoleDTO Role)
    {}
}