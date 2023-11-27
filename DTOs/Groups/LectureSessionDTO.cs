using GroupTracker.Enums;

namespace GroupTracker.DTOs.Groups;

public class LectureSessionDTO
{
    public List<Weekday> Days { get; set; }
    public string Time { get; set; }
    public string Auditorium { get; set; }
    public bool IsOnline { get; set; } = false;
}
