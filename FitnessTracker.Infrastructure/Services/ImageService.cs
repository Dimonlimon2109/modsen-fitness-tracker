
using FitnessTracker.Domain.Interfaces.Adapters;
using FitnessTracker.Domain.Interfaces.Services;

namespace FitnessTracker.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IRootPath _rootPath;
        public ImageService(IRootPath rootPath)
        {
            _rootPath = rootPath;
        }

        public async Task<string> UploadImageAsync(IImageFile image, CancellationToken ct = default)
        {
            if (!IsImage(image))
            {
                throw new ArgumentException("Переданный файл не является поддерживаемым изображением");
            }
            Directory.CreateDirectory(_rootPath.RootPath);

            var uniqueFileName = $"{Guid.NewGuid()}-{image.FileName}";
            var filePath = Path.Combine(_rootPath.RootPath, uniqueFileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fs, ct);
            }

            return Path.Combine(_rootPath.RootPath, uniqueFileName);
        }

        public Task DeleteImageAsync(string imagePath)
        {
            var filePath = Path.Combine(_rootPath.RootPath, imagePath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }
        private static bool IsImage(IImageFile file)
        {
            var validImageTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
            return validImageTypes.Contains(file.ContentType);
        }
    }
}
