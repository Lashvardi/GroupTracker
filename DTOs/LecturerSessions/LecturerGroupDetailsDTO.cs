using GroupTracker.DTOs.Groups;
using GroupTracker.Enums;

namespace GroupTracker.DTOs.LecturerSessions;

public class LecturerGroupDetailsDTO
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public GroupStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CurrentSession { get; set; }
    public int SessionsAmount { get; set; }
    public bool DoIStart { get; set; }
    public int PerWeek { get; set; }
    public int SessionsFilled { get; set; }
    public int? CurrentSyllabusTopicId { get; set; }

    public List<LectureSessionGetDTO> Sessions { get; set; }
}

public class LectureSessionGetDTO
{
    public int Id { get; set; }
    public Weekday Day { get; set; }
    public string Time { get; set; }
    public string Auditorium { get; set; }
    public bool IsOnline { get; set; }
    public bool IsAlternate { get; set; }
}
