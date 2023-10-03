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
            try
            {
                var result = await _lecturerService.RegisterLecturer(lecturer);
                return result != null ? Ok(result) : BadRequest(new { message = "Registration failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
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

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyLecturer(string email, string token)
        {
            try
            {
                var result = await _lecturerService.VerifyLecturer(email, token);
                return result != null ? Ok(result) : Unauthorized(new { message = "Invalid email or token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("full-info")]
        public async Task<IActionResult> GetLecturerInfoAsync(int lecturerId)
        {
            try
            {
                var result = await _lecturerService.GetLecturerInfoAsync(lecturerId);
                return result != null ? Ok(result) : Unauthorized(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("subjects/{lecturerId}")]
        public async Task<IActionResult> GetLecturerSubjects(int lecturerId)
        {
            try
            {
                var result = await _lecturerService.GetLecturerSubjects(lecturerId);
                return result != null ? Ok(result) : Unauthorized(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("companies/{lecturerId}")]
        public async Task<IActionResult> GetLecturerCompanies(int lecturerId)
        {
            try
            {
                var result = await _lecturerService.GetLecturerCompanies(lecturerId);
                return result != null ? Ok(result) : Unauthorized(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
