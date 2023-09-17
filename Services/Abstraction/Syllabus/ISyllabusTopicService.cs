using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Syllabus;

public interface ISyllabusTopicService
{
    Task<List<SyllabusTopic>> GetSyllabusTopicsByGroupIdAsync(int groupId);
    Task<SyllabusTopic> CreateSyllabusTopicAsync(SyllabusTopic newTopic);
}
