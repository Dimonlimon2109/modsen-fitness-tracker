
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Interfaces.Auth;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Domain.Interfaces.Services;

namespace FitnessTracker.Application.UseCases.Auth
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokensService _tokensService;

        public LoginUseCase(
            IUserRepository userRepository,
            IPasswordService passwordService,
            ITokensService tokensService
            )
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokensService = tokensService;
        }

        public async Task<LoginResponse> ExecuteAsync(
            LoginRequest loginRequest,
            CancellationToken ct = default
            )
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email, ct);

            if (user == null)
            {
                throw new UserNotFoundException("Пользователь не найден");
            }

            if (!_passwordService.ValidatePassword(loginRequest.Password, user.PasswordHash))
            {
                throw new InvalidPasswordException("Неверный пароль");
            }

            var accessToken = _tokensService.GenerateAccessToken(user);
            var refreshTokenModel = _tokensService.GenerateRefreshToken();

            user.RefreshToken = refreshTokenModel.RefreshToken;
            user.RefreshTokenExpiresAt = refreshTokenModel.ExpiresAt;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(ct);

            var loginResponse = new LoginResponse(accessToken, refreshTokenModel.RefreshToken);

            return loginResponse;
        }
    }
}
