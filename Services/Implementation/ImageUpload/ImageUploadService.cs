using GroupTracker.Services.Abstraction.ImageUpload;

namespace GroupTracker.Services.Implementation.ImageUpload;

public class ImageUploadService: IImageUploadService
{
    private readonly IWebHostEnvironment envinroment;

    public ImageUploadService(IWebHostEnvironment envinroment)
    {
        this.envinroment = envinroment;
    }

    public Tuple<int, string> SaveImage(IFormFile file)
    {
        var contentPath = this.envinroment.ContentRootPath;
        var path = Path.Combine(contentPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }


        //Extensions
        var ext = Path.GetExtension(file.FileName);
        var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".PNG", ".JPG", ".jpg" };
        if (!allowedExtensions.Contains(ext))
        {
            throw new Exception("Invalid image type");
        }
        string UniqueString = Guid.NewGuid().ToString();

        var newFileName = UniqueString + ext;
        var fileWithPath = Path.Combine(path, newFileName);

        var stream = new FileStream(fileWithPath, FileMode.Create);
        file.CopyTo(stream);
        stream.Close();
        var baseUrl = "https://localhost:7273/Images/";
        var fullImageUrl = baseUrl + newFileName;

        return new Tuple<int, string>(1, newFileName);
    }

    public void DeleteImage(string filename)
    {
        var imagePath = Path.Combine("E:\\GroupTracker\\uploads\\", filename);
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
        else
        {
            throw new FileNotFoundException("Image not found.", filename);
        }
    }
}
