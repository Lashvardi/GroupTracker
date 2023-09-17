namespace GroupTracker.Models;

// THIS IS FOR THE ALTERNATE LECTURE WEEKS FOR ME TO TRACK 
public class AlternateWeek
{
    public int Id { get; set; }
    public int WeekNumber { get; set; }
    public int LectureSessionId { get; set; }

    public LectureSession LectureSession { get; set; }
}
