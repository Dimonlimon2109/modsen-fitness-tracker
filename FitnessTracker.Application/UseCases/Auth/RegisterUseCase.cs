
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Domain.Interfaces.Services;

namespace FitnessTracker.Application.UseCases.Auth
{
    public class RegisterUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public RegisterUseCase(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task ExecuteAsync(
            RegisterRequest registerRequest,
            CancellationToken ct = default
            )
        {
            var userExists = await _userRepository.IsUserExistsAsync(registerRequest.Email, ct);
            if(userExists)
            {
                throw new ArgumentException("Пользователь с таким email уже существует");
            }

            var user = new UserEntity
            {
                Email = registerRequest.Email,
                PasswordHash = _passwordService.HashPassword(registerRequest.Password)
            };

            await _userRepository.AddAsync(user, ct);
            await _userRepository.SaveChangesAsync(ct);

        }
    }
}
