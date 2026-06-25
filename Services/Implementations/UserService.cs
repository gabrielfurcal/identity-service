using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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
                    var userMapped = _mapper.Map<UserDTO, User>(user);
                    var entity = await _context.Set<User>().Where(x => x.Email == userMapped.Email && 
                        new PasswordHasher<User>().VerifyHashedPassword(x, x.PasswordHash, userMapped.PasswordHash) == PasswordVerificationResult.Success)
                        .FirstOrDefaultAsync();

                    if (entity is null) throw new Exception("Credentials do not match");

                    return CreateToken(entity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating User. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
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