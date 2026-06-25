using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace identity_service.Services.Implementations
{
    public class RoleGroupService : BaseService<RoleGroup, int?, RoleGroupDTO>, IRoleGroupService
    {
        public RoleGroupService(IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
        }
    }
}