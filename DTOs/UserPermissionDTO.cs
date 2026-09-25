namespace identity_service.DTOs
{
    public record UserPermissionDTO(Guid Id, UserDTO User, PermissionDTO Permission)
    {}
}