namespace GroupTracker.Services.Abstraction.FileStorage;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file);
    Stream GetFile(string fileName);

}