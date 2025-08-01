
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Models;
using System.Security.Claims;

namespace FitnessTracker.Domain.Interfaces.Services
{
    public interface ITokensService
    {
        string GenerateAccessToken(UserEntity user);
        RefreshTokenModel GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
