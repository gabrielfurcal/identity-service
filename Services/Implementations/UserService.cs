using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace identity_service.Services.Implementations
{
    public class UserService : BaseService<User, Guid?, UserDTO>, IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
            this._configuration = configuration;
        }

        public async Task<string> Login(UserDTO user)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entity = await _context.Set<User>().Where(x => x.Email == user.Email)
                        .FirstOrDefaultAsync();

                    if (entity is null) throw new Exception("Credentials do not match");

                    if(!BC.Verify(user.Password, entity.PasswordHash)) throw new Exception("Credentials do not match");

                    var roles = await _context.UserRoleView.Where(x => x.UserId == entity.Id).ToListAsync();

                    return CreateToken(entity, roles);
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

        private string CreateToken(User user, List<UserRoleView> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}