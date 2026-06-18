using identity_service.Models;

namespace identity_service.Services.Contracts
{
    public interface IUserService : IBaseService<User, Guid?, UserDTO>
    {
        
    }
}