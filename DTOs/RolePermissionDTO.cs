namespace identity_service.DTOs
{
    public record RolePermissionDTO(int Id, RoleDTO Role, PermissionDTO Permission)
    {}
}