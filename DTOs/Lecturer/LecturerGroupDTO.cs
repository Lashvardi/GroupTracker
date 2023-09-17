
namespace GroupTracker.DTOs.Lecturer;

public class LecturerGroupDTO
{
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public int GroupCount { get; set; }
    public string TopicName { get; set; }

    public ICollection<GroupLectureSessionDTO> Sessions { get; set; }
}
