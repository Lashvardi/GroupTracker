using GroupTracker.DTOs.GroupLectureSession;
using System.Text.Json.Serialization;

namespace GroupTracker.Models;

public class LecturerGroup
{
    public int Id { get; set; }
    
    
    // CompanyName is the name of the company that group is from (STEP/TLANCER)
    public string CompanyName { get; set; }
    public string GroupName { get; set; }

    // GRADE IS FOR STEP ONLY
    public string Grade { get; set; }

    // Navigation Properties
    public int LecturerId { get; set; }
    [JsonIgnore]
    public Lecturer Lecturer { get; set; }

    public ICollection<GroupLectureSession> GroupLectureSessions { get; set; }
    public ICollection<SyllabusTopic> SyllabusTopics { get; set; }


    public int? CurrentSyllabusTopicId { get; set; }
    [JsonIgnore]
    public SyllabusTopic CurrentSyllabusTopic { get; set; }
}
