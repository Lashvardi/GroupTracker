
using GroupTracker.Enums;

namespace GroupTracker.DTOs.Lecturer;

public class LecturerGroupDTO
{
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public string TopicName { get; set; }
    public int? TopicId { get; set; }
    public GroupStatus Status { get; set; }
    public string HEXColor { get; set; }
    public string GroupType { get; set; }
    public int SessionsAmount { get; set; }

    public ICollection<GroupLectureSessionDTO> Sessions { get; set; }
}
