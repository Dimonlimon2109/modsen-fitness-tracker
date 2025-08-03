using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Interfaces.Workouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.API.Controllers
{
    [Authorize]
    [Route("api/workouts")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly ICreateWorkoutUseCase _createWorkoutUseCase;

        public WorkoutsController(ICreateWorkoutUseCase createWorkoutUseCase)
        {
            _createWorkoutUseCase = createWorkoutUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateWorkoutRequest createWorkoutRequest,
            CancellationToken ct = default
            )
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            await _createWorkoutUseCase.ExecuteAsync(createWorkoutRequest, userEmail, ct);

            return Created();
        }
    }
}
