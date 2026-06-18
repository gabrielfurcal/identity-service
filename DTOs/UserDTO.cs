namespace identity_service.Models
{
    public record UserDTO(Guid Id, 
                          string Email, 
                          string PasswordHash, 
                          bool IsActive, 
                          DateTime CreatedAt,
                          DateTime UpdatedAt, 
                          HashSet<RefreshTokenDTO> RefreshTokens, 
                          HashSet<UserRoleDTO> UserRoles, 
                          HashSet<UserGroupDTO> UserGroups)
    {}
}