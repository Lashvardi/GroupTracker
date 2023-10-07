using GroupTracker.DTOs.Groups;

namespace GroupTracker.DTOs.Lecturer;

public class LecturerDTO
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public List<string> Companies { get; set; }
    public List<string> Subjects { get; set; }
    public int GroupCount { get; set; }
    public ICollection<GroupNameWithId> groupNameWithIds { get; set; }
    public ICollection<LecturerGroupDTO> Groups { get; set; }

}
