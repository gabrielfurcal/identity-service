using AutoMapper;
using identity_service.Context;
using identity_service.Models;
using identity_service.DTOs;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using identity_service.Utils;

namespace identity_service.Services.Implementations
{
    public class RefreshTokenService : BaseService<RefreshToken, Guid?, RefreshTokenDTO>, IRefreshTokenService
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenService(IConfiguration configuration, IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
            this._configuration = configuration;
        }

        public async Task<LoginDTO> Validate(RefreshTokenDTO refreshToken)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var existingRefreshToken = await _context.Set<RefreshToken>()
                        .Where(x => x.TokenHash == refreshToken.TokenHash && x.ExpiresAt > DateTime.Now).FirstOrDefaultAsync();

                    if(existingRefreshToken is null) throw new Exception("Invalid RefreshToken");

                    var user = await _context.Set<User>().FindAsync(existingRefreshToken.UserId);

                    if(user is null) throw new Exception("Invalid User");

                    var roles = await _context.UserRoleView.Where(x => x.UserId == user.Id).ToListAsync();

                    string jwt = new JWTGenerator(_configuration).CreateToken(user, roles);

                    return new LoginDTO(jwt, refreshToken.TokenHash!);
                } 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error validating RefreshToken. Message: {ex.Message}");
                throw new Exception(ex.Message); 
            }
        }

        public async Task<RefreshTokenDTO> Generate(Guid userId, string? deviceInfo, string? ipAddress)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var existingRefreshToken = await _context.Set<RefreshToken>().Where(x => x.UserId == userId && x.ReplacedByTokenId == null).FirstOrDefaultAsync();

                    RefreshToken refreshToken = new RefreshToken()
                    {
                        Id = Guid.NewGuid(),
                        TokenHash = GenerateRefreshToken(),
                        CreatedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.AddDays(7),
                        RevokedAt = null,
                        ReplacedByTokenId = null,
                        DeviceInfo = deviceInfo,
                        IPAddress = ipAddress,
                        UserId = userId
                    };

                     await _context.Set<RefreshToken>().AddAsync(refreshToken);

                    if(existingRefreshToken is not null)
                    {
                        existingRefreshToken.ReplacedByTokenId = refreshToken.Id;
                        existingRefreshToken.RevokedAt = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();

                    return _mapper.Map<RefreshToken, RefreshTokenDTO>(refreshToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating RefreshToken. Message: {ex.Message}");
                throw new Exception(ex.Message); 
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}