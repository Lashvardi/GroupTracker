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

        [HttpPost("upload-profile-picture/{lecturerId}")]
        public async Task<IActionResult> UploadProfilePicture(int lecturerId, IFormFile file)
        {
            try
            {
                var lecturer = await _lecturerService.UploadProfilePictureAsync(lecturerId, file);
                return Ok(lecturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("upload-banner-picture/{lecturerId}")]
        public async Task<IActionResult> UploadbannerPicture(int lecturerId, IFormFile file)
        {
            try
            {
                var lecturer = await _lecturerService.UploadBannerPictureAsync(lecturerId, file);
                return Ok(lecturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile-picture/{lecturerId}")]
        public async Task<IActionResult> GetProfilePicture(int lecturerId)
        {
            try
            {
                var lecturer = await _lecturerService.GetProfilePictureAsync(lecturerId);
                return Ok(lecturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("banner-picture/{lecturerId}")]
        public async Task<IActionResult> GetBannerImageAsync(int lecturerId)
        {
            try
            {
                var lecturer = await _lecturerService.GetbannerImageAsync(lecturerId);
                return Ok(lecturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-profile-picture/{lecturerId}")]
        public IActionResult DeleteProfilePicture(int lecturerId)
        {
            try
            {
                _lecturerService.DeleteProfilePicture(lecturerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-banner-picture/{lecturerId}")]
        public IActionResult DeleteBannerPicture(int lecturerId)
        {
            try
            {
                _lecturerService.DeleteBannerPicture(lecturerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-socials/{lecturerId}")]
        public async Task<IActionResult> AddSocials(int lecturerId, LecturerSocialsDTO socials)
        {
            try
            {
                var result = await _lecturerService.AddSocialsAsync(lecturerId, socials);
                return result != null ? Ok(result) : Unauthorized(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("get-socials/{lecturerId}")]
        public async Task<IActionResult> GetSocials(int lecturerId)
        {
            try
            {
                var result = await _lecturerService.GetSocialsAsync(lecturerId);
                return result != null ? Ok(result) : Unauthorized(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("has-filled-out-socials/{lecturerId}")]
        public async Task<IActionResult> HasFilledOutSocials(int lecturerId)
        {
            try
            {
                var result = await _lecturerService.HasFilledOutSocialsAsync(lecturerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("delete-lecturer/{lecturerId}")]
        public async Task<IActionResult> DeleteLecturer(int lecturerId)
        {
            try
            {
                await _lecturerService.DeleteLecturerAsync(lecturerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
    