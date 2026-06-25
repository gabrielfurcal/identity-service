namespace identity_service.DTOs
{
    public record UserDTO(Guid Id, 
                          string Email, 
                          string Password, 
                          bool IsActive, 
                          DateTime CreatedAt,
                          DateTime UpdatedAt, 
                          HashSet<RefreshTokenDTO> RefreshTokens, 
                          HashSet<UserRoleDTO> UserRoles, 
                          HashSet<UserGroupDTO> UserGroups)
    {}
}