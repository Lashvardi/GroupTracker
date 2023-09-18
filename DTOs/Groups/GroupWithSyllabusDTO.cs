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

    // Flags For Icons in frontend
    public bool HasSyllabus { get; set; } // just check the length of the syllabus topics
    public bool IsOnline { get; set; }
    public bool IsAlternate { get; set; }


    public List<SyllabusDTO> SyllabusTopics { get; set; }  


}
