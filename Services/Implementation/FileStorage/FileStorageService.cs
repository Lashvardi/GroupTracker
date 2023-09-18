using GroupTracker.Services.Abstraction.FileStorage;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GroupTracker.Services.Implementation.FileStorage
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _uploadDir;

        public FileStorageService()
        {
            _uploadDir = "uploads"; 

            if (!Directory.Exists(_uploadDir))
            {
                Directory.CreateDirectory(_uploadDir);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty");
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(_uploadDir, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        public Stream GetFile(string fileName)
        {
            string filePath = Path.Combine(_uploadDir, fileName);
            if (File.Exists(filePath))
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            return null;
        }
    }
}
