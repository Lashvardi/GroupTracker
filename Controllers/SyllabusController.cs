using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Syllabus;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyllabusController : Controller
    {

        private readonly ISyllabusTopicService _syllabusService;

        public SyllabusController(ISyllabusTopicService syllabusService)
        {
            _syllabusService = syllabusService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSyllabusTopic(SyllabusTopic syllabusTopicDTO)
        {
            try
            {
                var result = await _syllabusService.CreateSyllabusTopicAsync(syllabusTopicDTO);
                return result != null ? Ok(result) : BadRequest(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("get-for-group/{groupId}")]
        public async Task<IActionResult> GetSyllabusTopicsForGroup(int groupId)
        {
            try
            {
                var result = await _syllabusService.GetSyllabusTopicsByGroupIdAsync(groupId);
                return result != null ? Ok(result) : BadRequest(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("get-by-id/{syllabusTopicId}")]
        public async Task<IActionResult> GetSyllabusTopicById(int syllabusTopicId)
        {
            try
            {
                var result = await _syllabusService.GetSyllabusTopicByIdAsync(syllabusTopicId);
                return result != null ? Ok(result) : BadRequest(new { message = "oops something went wrong ..." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
