using identity_service.Models;

namespace identity_service.Services.Contracts
{
    public interface IUserGroupService : IBaseService<UserGroup, Guid?, UserGroupDTO>
    {
        
    }
}