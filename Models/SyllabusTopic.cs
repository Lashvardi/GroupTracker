using GroupTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroupTracker.Models;

public class SyllabusTopic
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Details { get; set; }

    [Required]
    public int Order { get; set; }

    public int LecturerGroupId { get; set; }
    [JsonIgnore]
    public LecturerGroup LecturerGroup { get; set; }
}
