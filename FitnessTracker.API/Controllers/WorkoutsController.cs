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
        private readonly IGetAllWorkoutsUseCase _getAllWorkoutsUseCase;
        private readonly IGetWorkoutByIdUseCase _getWorkoutByIdUseCase;
        private readonly IDeleteWorkoutUseCase _deleteWorkoutUseCase;
        private readonly IUpdateWorkoutUseCase _updateWorkoutUseCase;
        public WorkoutsController(
            ICreateWorkoutUseCase createWorkoutUseCase,
            IGetAllWorkoutsUseCase getAllWorkoutsUseCase,
            IGetWorkoutByIdUseCase getWorkoutByIdUseCase,
            IDeleteWorkoutUseCase deleteWorkoutUseCase,
            IUpdateWorkoutUseCase updateWorkoutUseCase)
        {
            _createWorkoutUseCase = createWorkoutUseCase;
            _getAllWorkoutsUseCase = getAllWorkoutsUseCase;
            _getWorkoutByIdUseCase = getWorkoutByIdUseCase;
            _deleteWorkoutUseCase = deleteWorkoutUseCase;
            _updateWorkoutUseCase = updateWorkoutUseCase;
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

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllWorkoutsRequest getAllWorkoutsRequest,
            CancellationToken ct = default
            )
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userWorkouts = await _getAllWorkoutsUseCase.ExecuteAsync(getAllWorkoutsRequest, userEmail, ct);
            return Ok(userWorkouts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken ct = default)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var workout = await _getWorkoutByIdUseCase.ExecuteAsync(id, userEmail, ct);
            return Ok(workout);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken ct = default)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _deleteWorkoutUseCase.ExecuteAsync(id, userEmail, ct);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateWorkoutRequest updateWorkoutRequest,
            CancellationToken ct = default)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _updateWorkoutUseCase.ExecuteAsync(updateWorkoutRequest, userEmail, ct);
            return Ok();
        }
    }
}
