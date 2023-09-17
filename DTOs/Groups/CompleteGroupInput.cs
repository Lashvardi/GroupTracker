namespace GroupTracker.DTOs.Groups;

public class CompleteGroupInput
{
    public LectureGroupDTO Group { get; set; }
    public LectureSessionDTO Session { get; set; }
    public List<AlternateWeekDTO> AlternateWeeks { get; set; }
}
