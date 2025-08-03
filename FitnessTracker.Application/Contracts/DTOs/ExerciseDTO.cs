
namespace FitnessTracker.Application.Contracts.DTOs
{
    public record ExerciseDTO
        (
        string Name,
        IEnumerable<SetDTO> Sets
        );
}
