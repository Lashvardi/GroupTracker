using System.Text.Json.Serialization;

namespace GroupTracker.Models;

// THIS IS FOR THE ALTERNATE LECTURE WEEKS FOR ME TO TRACK 
public class AlternateWeek
{
    public int Id { get; set; }

    // CSV Format
    public int WeekNumber { get; set; }
    public int LectureSessionId { get; set; }
    public bool IsMyturn { get; set; } = false;

    // Navigation Properties

    [JsonIgnore]
    public LectureSession LectureSession { get; set; }
}
