using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUseCase _registerUseCase;

        public AuthController(IRegisterUseCase registerUseCase)
        {
            _registerUseCase = registerUseCase;
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
    }
}
