
namespace FitnessTracker.Application.Contracts.DTOs
{
    public class ExerciseDTO
    {
        public string Name {  get; set; }
        public List<SetDTO> Sets { get; set; }
    }
}
