namespace identity_service.DTOs
{
    public record UserDTO
    {
        public UserDTO()
        {
            
        }

        public UserDTO(Guid Id, string Email, string Password, bool IsActive, DateTime CreatedAt, DateTime UpdatedAt, HashSet<RefreshTokenDTO> RefreshTokens, HashSet<UserRoleDTO> UserRoles,
        HashSet<UserGroupDTO> UserGroups)
        {
            this.Id = Id;
            this.Email = Email;
            this.Password = Password;
            this.IsActive = IsActive;
            this.CreatedAt = CreatedAt;
            this.UpdatedAt = UpdatedAt;
            this.RefreshTokens = RefreshTokens;
            this.UserRoles = UserRoles;
            this.UserGroups = UserGroups;
        }

        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public HashSet<RefreshTokenDTO> RefreshTokens { get; init; } = [];
        public HashSet<UserRoleDTO> UserRoles { get; init; } = [];
        public HashSet<UserGroupDTO> UserGroups { get; init; } = [];
    }
}