using GroupTracker.Enums;

namespace GroupTracker.DTOs.Groups;

public class LectureGroupDTO
{
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public GroupStatus Status { get; set; }
    public int SessionsAmount { get; set; }
    public bool DoIStart { get; set; }
    public DateTime StartDate { get; set; }
    public int PerWeek { get; set; }

}
