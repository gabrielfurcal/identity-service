namespace identity_service.Models
{
    public record RefreshTokenDTO(Guid Id, 
                                  string TokenHash, 
                                  DateTime CreatedAt, 
                                  DateTime ExpiresAt, 
                                  DateTime RevokedAt, 
                                  Guid? ReplacedByTokenId, 
                                  string? DeviceInfo, 
                                  string? IPAddress, 
                                  UserDTO User)
    {}
}