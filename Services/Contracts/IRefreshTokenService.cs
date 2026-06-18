using identity_service.Models;

namespace identity_service.Services.Contracts
{
    public interface IRefreshTokenService : IBaseService<RefreshToken, Guid?, RefreshTokenDTO>
    {
        
    }
}