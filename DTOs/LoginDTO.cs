namespace identity_service.DTOs
{
    public record LoginDTO(string Jwt, string RefreshToken)
    {}
}