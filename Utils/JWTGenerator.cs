using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using identity_service.DTOs;
using identity_service.Models;
using Microsoft.IdentityModel.Tokens;

namespace identity_service.Utils
{
    public class JWTGenerator
    {
        public IConfiguration _configuration;

        public JWTGenerator(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string CreateToken(User user, List<UserRoleView> roles)
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