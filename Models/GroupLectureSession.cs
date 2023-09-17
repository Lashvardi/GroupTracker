using GroupTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GroupTracker.DTOs.GroupLectureSession;

public class GroupLectureSession
{
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }
    public LecturerGroup Group { get; set; }

    public int LectureSessionId { get; set; }
    public LectureSession LectureSession { get; set; }
}