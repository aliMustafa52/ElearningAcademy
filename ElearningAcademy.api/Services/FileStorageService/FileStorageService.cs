
namespace ElearningAcademy.api.Services.FileStorageService
{
    public class FileStorageService(IWebHostEnvironment webHostEnvironment) : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/images";

        public async Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            var path = Path.Combine(_imagesPath, image.FileName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream, cancellationToken);

            return path;
        }

        public Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            if(File.Exists(fileUrl))
                File.Delete(fileUrl);

            return Task.CompletedTask;
        }


    }
}
