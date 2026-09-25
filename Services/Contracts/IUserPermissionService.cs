using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Services.Contracts
{
    public interface IUserPermissionService : IBaseService<UserPermission, Guid?, UserPermissionDTO>
    {
        
    }
}