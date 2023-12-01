using GroupTracker.data;
using GroupTracker.DTOs.Friends;
using GroupTracker.Services.Abstraction.Friends;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.Services.Implementation.Friends;

public class FriendService : IFriendsService
{
    private readonly DataContext _context;

    public FriendService(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<IEnumerable<FriendDTO>> GetOtherLecturersWithSimilarInterestsAndSkills(int lecturerId)
    {
        var lecturer = await _context.Lecturers
            .FirstOrDefaultAsync(l => l.Id == lecturerId);

        if (lecturer == null)
        {
            // Handle the case where the lecturer is not found
            return Enumerable.Empty<FriendDTO>();
        }

        // Assuming Subjects and Companies are CSV strings
        var subjectNames = lecturer.Subjects.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        var companyNames = lecturer.Companies.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList();

        var otherLecturersWithSimilarInterestsAndSkills = await _context.Lecturers
            .Where(l => l.Id != lecturerId)
            .ToListAsync();

        // Filtering based on at least one similar subject or company
        var filteredLecturers = otherLecturersWithSimilarInterestsAndSkills
            .Where(l => l.Subjects.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Any(s => subjectNames.Contains(s.Trim())) ||
                        l.Companies.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Any(c => companyNames.Contains(c.Trim())))
            .Select(l => new FriendDTO
            {
                Id = l.Id,
                ImageUrl = l.ImageUrl,
                FullName = l.FirstName + " " + l.LastName, // Assuming FirstName and LastName need to be concatenated
                Companies = l.Companies,
                Subjects = l.Subjects,
                GroupCount = l.LecturerGroups?.Count ?? 0
            }).ToList();

        return filteredLecturers;
    }

    public async Task<FriendProfileDTO> GetFriendProfile(int friendId)
    {
        var friend = await _context.Lecturers
            .FirstOrDefaultAsync(l => l.Id == friendId);

        if (friend == null)
        {
            // Handle the case where the friend is not found
            return null;
        }

        var friendProfile = new FriendProfileDTO
        {
            Id = friend.Id,
            ImageUrl = friend.ImageUrl,
            FullName = friend.FirstName + " " + friend.LastName, // Assuming FirstName and LastName need to be concatenated
            Companies = friend.Companies,
            Subjects = friend.Subjects,
            GroupCount = friend.LecturerGroups?.Count ?? 0,
            Facebook = friend.FacebookLink,
            Instagram = friend.InstagramLink,
            LinkedIn = friend.LinkedInLink,
            Twitter = friend.TwitterLink,
            Website = friend.PersonalWebsiteLink,
            YouTube = friend.YouTubeLink,
            Email = friend.Email

        };

        return friendProfile;
    }



}
