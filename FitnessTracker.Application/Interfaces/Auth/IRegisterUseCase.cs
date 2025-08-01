using FitnessTracker.Application.Contracts.Requests;

namespace FitnessTracker.Application.Interfaces.Auth
{
    public interface IRegisterUseCase
    {
        Task ExecuteAsync(RegisterRequest registerRequest, CancellationToken ct = default);
    }
}