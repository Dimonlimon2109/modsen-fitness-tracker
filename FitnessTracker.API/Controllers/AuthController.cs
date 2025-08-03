using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUseCase _registerUseCase;
        private readonly ILoginUseCase _loginUseCase;
        private readonly IRefreshTokensUseCase _refreshTokensUseCase;

        public AuthController(
            IRegisterUseCase registerUseCase,
            ILoginUseCase loginUseCase,
            IRefreshTokensUseCase refreshTokensUseCase
            )
        {
            _registerUseCase = registerUseCase;
            _loginUseCase = loginUseCase;
            _refreshTokensUseCase = refreshTokensUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest registerRequest,
            CancellationToken ct = default
            )
        {
            await _registerUseCase.ExecuteAsync(registerRequest, ct);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest loginRequest,
            CancellationToken ct = default
            )
        {
            var loginResponse = await _loginUseCase.ExecuteAsync(loginRequest, ct);
            return Ok(loginResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
            RefreshTokensRequest refreshTokensRequest,
            CancellationToken ct = default
            )
        {
            var refreshTokensResponse = await _refreshTokensUseCase.ExecuteAsync(refreshTokensRequest, ct);
            return Ok(refreshTokensResponse);
        }
    }
}
