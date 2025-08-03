
namespace FitnessTracker.Domain.Interfaces.Adapters
{
    public interface IImageFile
    {
        string FileName { get; }
        string ContentType { get; }
        long Length { get; }
        Task CopyToAsync(FileStream target, CancellationToken ct = default);
    }
}
