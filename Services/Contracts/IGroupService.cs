using identity_service.DTOs;
using identity_service.Models;

namespace identity_service.Services.Contracts
{
    public interface IGroupService : IBaseService<Group, int?, GroupDTO>
    {
        
    }
}