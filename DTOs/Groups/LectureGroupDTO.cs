namespace GroupTracker.DTOs.Groups;

public class LectureGroupDTO
{
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public int LecturerId { get; set; }
    public ICollection<LectureSessionDTO> LectureSessions { get; set; }
}
