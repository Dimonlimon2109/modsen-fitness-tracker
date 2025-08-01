
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Domain.Interfaces.Services;

namespace FitnessTracker.Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public LoginUseCase(
            IUserRepository userRepository,
            IPasswordService passwordService
            )
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }
    }
}
