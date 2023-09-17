using GroupTracker.data;
using GroupTracker.DTOs.GroupLectureSession;
using GroupTracker.DTOs.Groups;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Group;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.Services.Implementation.Group;

public class LecturerGroupCoordinator : ILecturerGroupCoordinator
{
    private readonly IGroupService _groupService;
    private readonly ILectureSessionService _lectureSessionService;
    private readonly IAlternateWeekService _alternateWeekService;
    private readonly DataContext _context;

    public LecturerGroupCoordinator(IGroupService groupService, ILectureSessionService lectureSessionService, IAlternateWeekService alternateWeekService, DataContext context)
    {
        _groupService = groupService;
        _lectureSessionService = lectureSessionService;
        _alternateWeekService = alternateWeekService;
        _context = context;
    }

    public async Task<LecturerGroup> CreateAndAssignGroup(int lecturerId, CompleteGroupInput completeGroupInput)
    {
        var lecturer = await _context.Lecturers
                        .FirstOrDefaultAsync(x => x.Id == lecturerId);
        if (lecturer == null)
        {
            throw new Exception("Lecturer not found");
        }

        var newGroup = await _groupService.CreateGroup(completeGroupInput);
        newGroup.LecturerId = lecturerId;

        var newSession = await _lectureSessionService.CreateLectureSession(completeGroupInput.Session);

        List<AlternateWeek> newAlternateWeeks = null;
        if (newSession.IsAlternate)
        {
            newAlternateWeeks = await _alternateWeekService.CreateAlternateWeeks(completeGroupInput.AlternateWeeks);
        }

        var newGroupLectureSession = new GroupLectureSession
        {
            Group = newGroup,
            LectureSession = newSession
        };

        newGroup.GroupLectureSessions = new List<GroupLectureSession> { newGroupLectureSession };
        newSession.AlternateWeeks = newAlternateWeeks;

        await _context.LecturerGroups.AddAsync(newGroup);
        await _context.GroupLectureSessions.AddAsync(newGroupLectureSession);
        await _context.SaveChangesAsync();

        return newGroup;
    }
}
