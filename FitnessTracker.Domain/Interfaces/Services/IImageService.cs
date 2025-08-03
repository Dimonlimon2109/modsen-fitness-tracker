
using FitnessTracker.Domain.Interfaces.Adapters;

namespace FitnessTracker.Infrastructure.Services
{
    public interface IImageService
    {
        Task DeleteImageAsync(string imagePath);
        Task<string> UploadImageAsync(IImageFile image, CancellationToken ct = default);
    }
}