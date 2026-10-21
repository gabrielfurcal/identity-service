using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Services.Contracts
{
    public interface IRolePermissionService : IBaseService<RolePermission, int?, RolePermissionDTO>
    {
        
    }
}