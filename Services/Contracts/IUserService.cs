using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Services.Contracts
{
    public interface IUserService : IBaseService<User, Guid?, UserDTO>
    {
        public Task<LoginDTO> Login(UserDTO user, string deviceInfo, string? ipAddress);
    }
}