using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace identity_service.Services.Implementations
{
    public class GroupService : BaseService<Group, int?, GroupDTO>, IGroupService
    {
        public GroupService(IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
        }
    }
}