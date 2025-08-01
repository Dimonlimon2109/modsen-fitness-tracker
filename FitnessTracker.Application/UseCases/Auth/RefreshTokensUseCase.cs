
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;
using FitnessTracker.Application.Exceptions;
using FitnessTracker.Application.Interfaces.Auth;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FitnessTracker.Application.UseCases.Auth
{
    public class RefreshTokensUseCase : IRefreshTokensUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokensService _tokensService;

        public RefreshTokensUseCase(
            IUserRepository userRepository,
            ITokensService tokensService
            )
        {
            _userRepository = userRepository;
            _tokensService = tokensService;
        }

        public async Task<RefreshTokensResponse> ExecuteAsync(
            RefreshTokensRequest refreshTokensRequest,
            CancellationToken ct = default
            )
        {
            var email = _tokensService.GetEmailFromToken(refreshTokensRequest.AccessToken);

            if (email == null)
            {
                throw new InvalidTokenException("Неверный access-token");
            }
            var user = await _userRepository.GetUserByEmailAsync(email, ct);

            if (user == null)
            {
                throw new UserNotFoundException("Пользователь не найден");
            }

            if (user.RefreshToken != refreshTokensRequest.RefreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
            {
                throw new InvalidTokenException("Неверный refresh-token");
            }

            var refreshToken = _tokensService.GenerateRefreshToken();
            user.RefreshToken = refreshToken.RefreshToken;
            user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(ct);

            var accessToken = _tokensService.GenerateAccessToken(user);
            return new RefreshTokensResponse(accessToken, refreshToken.RefreshToken);
        }
    }
}
