using GroupTracker.DTOs.Syllabus;
using GroupTracker.Enums;
using GroupTracker.Models;

namespace GroupTracker.DTOs.Groups;

public class GroupWithSyllabusDTO
{
    public string CompanyName { get; set; }
    public string GroupName { get; set; }
    public string Grade { get; set; }
    public GroupStatus Status { get; set; }
    public int SessionsAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CurrentSession { get; set; }
    public string HEX { get; set; }
    public string GroupType { get; set; }
    public string Auditorium { get; set; }

    // Flags For Icons in frontend
    public bool HasSyllabus { get; set; } // just check the length of the syllabus topics
    public bool IsOnline { get; set; }
    public bool IsAlternate { get; set; }


    public List<SyllabusDTO> SyllabusTopics { get; set; }  


}
