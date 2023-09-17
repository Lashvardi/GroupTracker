namespace GroupTracker.DTOs.Lecturer;

public class GroupLectureSessionDTO
{
    public string SessionDate { get; set; }
    public string SessionTime { get; set; }
    public bool IsOnline { get; set; }
    public bool IsAlternate { get; set; }
}
