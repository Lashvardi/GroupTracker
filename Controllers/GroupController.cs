using GroupTracker.DTOs.Groups;
using GroupTracker.Services.Abstraction.Group;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : Controller
    {

        private readonly ILecturerGroupCoordinator _lecturerGroupCoordinator;

        public GroupController(ILecturerGroupCoordinator lecturerGroupCoordinator)
        {
            _lecturerGroupCoordinator = lecturerGroupCoordinator;
        }


        [HttpPost("create-and-assign")]
        public async Task<IActionResult> CreateAndAssignGroup(int lecturerId, [FromBody] CompleteGroupInput completeGroupInput)
        {
            try
            {
                var newGroup = await _lecturerGroupCoordinator.CreateAndAssignGroup(lecturerId, completeGroupInput);
                return Created($"Group created with ID: {newGroup.Id}", newGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            try
            {
                var deletedGroup = await _lecturerGroupCoordinator.DeleteGroup(groupId);
                return Ok(deletedGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("change-topic")]
        public async Task<IActionResult> ChangeGroupTopic(int groupId, int topicId)
        {
            try
            {
                var changedGroup = await _lecturerGroupCoordinator.ChangeGroupTopic(groupId, topicId);
                return Ok(changedGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-for-lecturer")]
        public async Task<IActionResult> GetLecturerGroups(int lecturerId)
        {
            try
            {
                var groups = await _lecturerGroupCoordinator.GetAllGroupsWithSyllabusForLecturer(lecturerId);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-sessions-for-lecturer")]
        public async Task<IActionResult> GetGroupDetailsWithSessionsForLecturer(int lecturerId)
        {
            try
            {
                var group = await _lecturerGroupCoordinator.GetGroupDetailsWithSessionsForLecturer(lecturerId);
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
