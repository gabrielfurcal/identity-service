using identity_service.Models;

namespace identity_service.Services.Contracts
{
    public interface IRoleService : IBaseService<Role, int?, RoleDTO>
    {
        
    }
}