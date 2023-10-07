using GroupTracker.DTOs.GroupLectureSession;
using GroupTracker.Enums;
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
    public GroupStatus Status { get; set; } = GroupStatus.Active;



    public int CurrentSession { get; set; } = 0;
    public int SessionsAmount { get; set; } = 0;
    public bool DoIStart { get;set; } = false;

    public int PerWeek { get; set; } = 0;
    public int SessionsFilled { get; set; } = 0;

    public DateTime StartDate { get; set; }

    //TODO: In Frontend Make Dropdown of lengths for the groups and automatically send that amount +6 month or sm like that
    public DateTime EndDate { get; set; }

    public int? CurrentSyllabusTopicId { get; set; }


    // Navigation Properties
    public int LecturerId { get; set; }

    [JsonIgnore]
    public Lecturer Lecturer { get; set; }

    public ICollection<GroupLectureSession> GroupLectureSessions { get; set; }
    public ICollection<SyllabusTopic> SyllabusTopics { get; set; }


    [JsonIgnore]
    public SyllabusTopic CurrentSyllabusTopic { get; set; }
}
