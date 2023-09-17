using GroupTracker.data;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Syllabus;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.Services.Implementation.Syllabus;

public class SyllabusTopicService : ISyllabusTopicService
{
    private readonly DataContext _context;

    public SyllabusTopicService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<SyllabusTopic>> GetSyllabusTopicsByGroupIdAsync(int groupId)
    {
        if (groupId == 0)
        {
            throw new Exception("Invalid group id");
        }


        return await _context.SyllabusTopics
                             .Where(st => st.LecturerGroupId == groupId)
                             .OrderBy(st => st.Order)
                             .ToListAsync();
    }

    public async Task<SyllabusTopic> CreateSyllabusTopicAsync(SyllabusTopic newTopic)
    {
        var lastOrder = await _context.SyllabusTopics
                                      .Where(st => st.LecturerGroupId == newTopic.LecturerGroupId)
                                      .OrderByDescending(st => st.Order)
                                      .Select(st => st.Order)
                                      .FirstOrDefaultAsync();

        if (newTopic.LecturerGroupId == 0)
        {
            throw new Exception("Invalid group id");
        }


        newTopic.Order = lastOrder + 1;

        _context.SyllabusTopics.Add(newTopic);
        await _context.SaveChangesAsync();

        return newTopic;
    }

    public async Task<SyllabusTopic> GetSyllabusTopicByIdAsync(int topicId)
    {
        if (topicId == 0)
        {
            throw new Exception("Invalid topic id");
        }

        return await _context.SyllabusTopics
                             .FirstOrDefaultAsync(st => st.Id == topicId);
    }



}
