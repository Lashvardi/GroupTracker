using GroupTracker.DTOs.Friends;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Friends;

public interface IFriendsService
{
    // Get Other Lecturers with similar interests and skills
    Task<IEnumerable<FriendDTO>> GetOtherLecturersWithSimilarInterestsAndSkills(int lecturerId);
    // get friends profile
    Task<FriendProfileDTO> GetFriendProfile(int friendId);

    // get messages
    Task<IEnumerable<ChatMessage>> GetMessages(string senderId, string receiverId);
}
