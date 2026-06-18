using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace identity_service.Services.Implementations
{
    public class RefreshTokenService : BaseService<RefreshToken, Guid?, RefreshTokenDTO>, IRefreshTokenService
    {
        public RefreshTokenService(IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
        }
    }
}