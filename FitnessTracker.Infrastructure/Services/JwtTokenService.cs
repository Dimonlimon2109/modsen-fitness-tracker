using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Services;
using FitnessTracker.Domain.Models;
using FitnessTracker.Infrastructure.Services.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessTracker.Infrastructure.Services
{
    public class JwtTokenService : ITokensService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateAccessToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                   [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifetimeMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshTokenModel GenerateRefreshToken()
        {
            return new RefreshTokenModel
            {
                RefreshToken = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetimeDays)
            };
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Неверный токен");
            }
            return principal;
        }
    }
}
