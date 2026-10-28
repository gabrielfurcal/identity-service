using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using identity_service.Utils;

namespace identity_service.Services.Implementations
{
    public class UserService : BaseService<User, Guid?, UserDTO>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserService(IConfiguration configuration, IRefreshTokenService refreshTokenService, IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
            this._configuration = configuration;
            this._refreshTokenService = refreshTokenService;
        }

        public async Task<UserPageResponse> FindAll(int offset, int limit)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var totalCount = await _context.User.CountAsync();
                    var users = await _context.User.Where(x => x.IsActive == true).Skip(offset * limit).Take(limit).Select(e => _mapper.Map<User, UserDTO>(e)).ToListAsync();
                    return new UserPageResponse { Users = users, TotalCount = totalCount };
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding List of User. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoginDTO> Login(UserDTO user, string deviceInfo, string? ipAddress)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entity = await _context.Set<User>().Where(x => x.Email == user.Email)
                        .FirstOrDefaultAsync();

                    if (entity is null) throw new Exception("Credentials do not match");

                    if(!BC.Verify(user.Password, entity.PasswordHash)) throw new Exception("Credentials do not match");

                    var permissions = await _context.UserPermissionView.Where(x => x.UserId == entity.Id).ToListAsync();

                    var jwt = new JWTGenerator(_configuration).CreateToken(entity, permissions);
                    var refreshToken = await _refreshTokenService.Generate(entity.Id, deviceInfo, ipAddress);

                    return new LoginDTO(jwt, refreshToken.TokenHash!);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating User. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public override async Task<UserDTO> Save(UserDTO dto, Guid? id)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    User? entity;

                    if (id is null) 
                    {
                        entity = _mapper.Map<UserDTO, User>(dto);
                        entity.PasswordHash = BC.HashPassword(dto.Password);
                        await _context.Set<User>().AddAsync(entity!);
                    }
                    else
                    {
                        entity = await _context.Set<User>().FindAsync(id);
                        _mapper.Map(dto, entity!);
                    }

                    await _context.SaveChangesAsync();

                    return _mapper.Map<User, UserDTO>(entity!);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving entity of User. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }              
        }

        public async Task<bool> DeactivateById(Guid id)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entity = await _context.User.FindAsync(id);

                    if (entity != null)
                    {
                        entity.IsActive = false;
                        await _context.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error deactivating element of User, with ID: {id}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }    
        }
    }
}