using GroupTracker.DTOs.GroupLectureSession;

namespace GroupTracker.Models;

public class LectureSession
{
    public int Id { get; set; }


    public string Day { get; set; }
    public string Time { get; set; }

    public string Auditorium { get; set; }
    public bool IsOnline { get; set; } = false;
    public bool IsAlternate { get; set; } = false;

    public ICollection<GroupLectureSession> GroupLectureSessions { get; set; }
    public ICollection<AlternateWeek> AlternateWeeks { get; set; } = new List<AlternateWeek>();

}
