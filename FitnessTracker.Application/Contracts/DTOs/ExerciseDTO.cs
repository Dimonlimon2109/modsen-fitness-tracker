
namespace FitnessTracker.Application.Contracts.DTOs
{
    public record ExerciseDTO
        (
        string Name,
        List<SetDTO> Sets
        );
}
