using identity_service.Models;

namespace identity_service.Services.Contracts
{
    public interface IUserRoleService : IBaseService<UserRole, Guid?, UserRoleDTO>
    {
        
    }
}