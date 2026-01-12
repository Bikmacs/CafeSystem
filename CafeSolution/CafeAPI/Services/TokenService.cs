using CafeAPI.Configuration;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Login), 
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var signingKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials( 
                signingKey, 
                SecurityAlgorithms.HmacSha256Signature
            );

            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifeTimeInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), 
                Expires = expires, 
                Issuer = _jwtSettings.Issuer, 
                Audience = _jwtSettings.Audience, 
                SigningCredentials = credentials 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
