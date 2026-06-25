using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Services.Contracts
{
    public interface IRefreshTokenService : IBaseService<RefreshToken, Guid?, RefreshTokenDTO>
    {
        
    }
}