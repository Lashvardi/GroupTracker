using GroupTracker.Enums;

namespace GroupTracker.DTOs.Lecturer;

public class GroupLectureSessionDTO
{
    public Weekday SessionDate { get; set; }
    public string SessionTime { get; set; }
    public bool IsOnline { get; set; }
    public bool IsAlternate { get; set; }
    public string Auditorium { get; set; }
}
