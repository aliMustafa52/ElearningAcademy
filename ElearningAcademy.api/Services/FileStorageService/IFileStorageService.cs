namespace ElearningAcademy.api.Services.FileStorageService
{
    public interface IFileStorageService
    {
        Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default);
        Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default);
    }
}
