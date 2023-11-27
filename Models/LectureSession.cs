using GroupTracker.DTOs.GroupLectureSession;
using GroupTracker.Enums;

namespace GroupTracker.Models;

public class LectureSession
{
    public int Id { get; set; }


    public Weekday Day { get; set; }
    public string Time { get; set; }

    public string Auditorium { get; set; }
    public bool IsOnline { get; set; } = false;

    public ICollection<GroupLectureSession> GroupLectureSessions { get; set; }

}
