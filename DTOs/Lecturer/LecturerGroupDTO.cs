
using GroupTracker.Enums;

namespace GroupTracker.DTOs.Lecturer;

public class LecturerGroupDTO
{
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public string TopicName { get; set; }
    public GroupStatus Status { get; set; }

    public ICollection<GroupLectureSessionDTO> Sessions { get; set; }
}
