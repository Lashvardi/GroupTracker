using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GroupTracker.Models;

public class FileAttachment
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }

    [NotMapped]
    public IFormFile File { get; set; }

    public int SyllabusTopicId { get; set; }
    [JsonIgnore]
    public SyllabusTopic SyllabusTopic { get; set; }
}
