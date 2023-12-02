using GroupTracker.Services.Abstraction.Friends;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendsController : Controller
    {
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet("get-other-lecturers-with-similar-interests-and-skills/{lecturerId}")]
        public async Task<IActionResult> GetOtherLecturersWithSimilarInterestsAndSkills(int lecturerId)
        {
            try
            {
                var result = await _friendsService.GetOtherLecturersWithSimilarInterestsAndSkills(lecturerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-friend-profile/{friendId}")]
        public async Task<IActionResult> GetFriendProfile(int friendId)
        {
            try
            {
                var result = await _friendsService.GetFriendProfile(friendId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-messages/{senderId}/{receiverId}")]
        public async Task<IActionResult> GetMessages(string senderId, string receiverId)
        {
            try
            {
                var result = await _friendsService.GetMessages(senderId, receiverId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
