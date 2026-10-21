using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Services.Contracts
{
    public interface IRoleGroupService : IBaseService<RoleGroup, int?, RoleGroupDTO>
    {
        
    }
}