namespace GroupTracker.Services.Abstraction.ImageUpload;

public interface IImageUploadService
{
    public Tuple<int, string> SaveImage(IFormFile imageFile);
    void DeleteImage(string filename);


}

