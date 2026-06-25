namespace identity_service.DTOs
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