using GroupTracker.DTOs.GroupLectureSession;

namespace GroupTracker.Models;

public class LecturerGroup
{
    public int Id { get; set; }
    
    
    public string GroupName { get; set; }
    public string IsOnline { get; set; }

    // Navigation Properties
    public int LecturerId { get; set; }
    public Lecturer Lecturer { get; set; }

    public ICollection<GroupLectureSession> GroupLectionDays { get; set; }

}
