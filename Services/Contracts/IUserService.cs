using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Services.Contracts
{
    public interface IUserService : IBaseService<User, Guid?, UserDTO>
    {
        public Task<string> Login(UserDTO user);
    }
}