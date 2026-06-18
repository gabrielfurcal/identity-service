using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace identity_service.Services.Implementations
{
    public class UserRoleService : BaseService<UserRole, Guid?, UserRoleDTO>, IUserRoleService
    {
        public UserRoleService(IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
        }
    }
}