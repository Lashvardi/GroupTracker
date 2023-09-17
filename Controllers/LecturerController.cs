using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LecturerController : Controller
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterLecturer(LecturerRegistrationInput lecturer)
        {
            var result = await _lecturerService.RegisterLecturer(lecturer);
            return result != null ? Ok(result) : BadRequest(new { message = "Registration failed" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginLecturer(string email, string password)
        {
            try
            {
                var result = await _lecturerService.LoginLecturer(email, password);
                return result != null ? Ok(result) : Unauthorized(new { message = "Invalid email or password" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
