using GroupTracker.Services.Abstraction.FileStorage;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : Controller
    {
        private readonly IFileStorageService _fileService;

        public FilesController(IFileStorageService fileService)
        {
            _fileService = fileService;
        }



        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            try
            {
                var fileStream = _fileService.GetFile(fileName);
                if (fileStream == null)
                {
                    return NotFound();
                }
                return File(fileStream, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
