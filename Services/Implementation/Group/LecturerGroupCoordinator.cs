using GroupTracker.data;
using GroupTracker.DTOs.GroupLectureSession;
using GroupTracker.DTOs.Groups;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.DTOs.LecturerSessions;
using GroupTracker.DTOs.Syllabus;
using GroupTracker.Enums;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Group;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.Services.Implementation.Group;

public class LecturerGroupCoordinator : ILecturerGroupCoordinator
{
    private readonly IGroupService _groupService;
    private readonly ILectureSessionService _lectureSessionService;
    private readonly DataContext _context;

    public LecturerGroupCoordinator(IGroupService groupService, ILectureSessionService lectureSessionService, DataContext context)
    {
        _groupService = groupService;
        _lectureSessionService = lectureSessionService;
        _context = context;
    }

    public async Task<LecturerGroup> CreateAndAssignGroup(int lecturerId, CompleteGroupInput completeGroupInput)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == lecturerId);
        if (lecturer == null)
        {
            throw new Exception("Lecturer not found");
        }

        var newGroup = await _groupService.CreateGroup(completeGroupInput);
        newGroup.LecturerId = lecturerId;

        List<LectureSession> allSessions = new List<LectureSession>();
        List<GroupLectureSession> allGroupLectureSessions = new List<GroupLectureSession>();
        List<Weekday> days = completeGroupInput.Session.Days;

        int totalSessions = completeGroupInput.Group.SessionsAmount;
        int sessionsPerWeek = completeGroupInput.Group.PerWeek;
        int totalWeeks = totalSessions / sessionsPerWeek;



        for (int week = 0; week < totalWeeks; week++)
        {
            for (int sessionIndex = 0; sessionIndex < sessionsPerWeek ; sessionIndex++)
            {
                int dayIndex = sessionIndex % days.Count;
                Weekday day = days[dayIndex];

                var session = new LectureSession
                {
                    Auditorium = completeGroupInput.Session.Auditorium,
                    Day = day, // Use the calculated day
                    Time = completeGroupInput.Session.Time,
                    IsOnline = completeGroupInput.Session.IsOnline,
                };
                await _lectureSessionService.CreateLectureSession(completeGroupInput.Session, day);
                allSessions.Add(session);

                allGroupLectureSessions.Add(new GroupLectureSession
                {
                    Group = newGroup,
                    LectureSession = session,
                    GroupId = newGroup.Id,
                    LectureSessionId = session.Id
                });
            }
        }



        newGroup.GroupLectureSessions = allGroupLectureSessions;


        await _context.LecturerGroups.AddAsync(newGroup);
        await _context.LectureSessions.AddRangeAsync(allSessions);
        await _context.GroupLectureSessions.AddRangeAsync(allGroupLectureSessions);
        await _context.SaveChangesAsync();

        return newGroup;
    }



    public async Task<LecturerGroup> DeleteGroup(int groupId)
    {
        var group = await _context.LecturerGroups
                    .Include(x => x.GroupLectureSessions)
                    .ThenInclude(x => x.LectureSession)
                    .FirstOrDefaultAsync(x => x.Id == groupId);

        if (group == null)
        {
            throw new Exception("Group not found");
        }

        _context.LecturerGroups.Remove(group);
        await _context.SaveChangesAsync();

        return group;
    }

    // changing the topic of the group (Sylabuss)
    public async Task<LecturerGroup> ChangeGroupTopic(int groupId, int topicId)
    {
        var group = await _context.LecturerGroups
                    .Include(x => x.GroupLectureSessions)
                    .ThenInclude(x => x.LectureSession)
                    .FirstOrDefaultAsync(x => x.Id == groupId);

        if (group == null)
        {
            throw new Exception("Group not found");
        }

        var topic = await _context.SyllabusTopics.FirstOrDefaultAsync(x => x.Id == topicId);
        group.CurrentSyllabusTopic = topic ?? throw new Exception("Topic not found");

        group.CurrentSyllabusTopicId = topic.Id;
        await _context.SaveChangesAsync();

        return group;
    }


    //getting the groip with sylabuss topics
    public async Task<List<GroupWithSyllabusDTO>> GetAllGroupsWithSyllabusForLecturer(int lecturerId)
    {
        var groups = await _context.LecturerGroups
                    .Where(x => x.LecturerId == lecturerId)
                    .Include(x => x.SyllabusTopics)
                    .Include(x => x.GroupLectureSessions)

                        .ThenInclude(gl => gl.LectureSession)
                    .ToListAsync();

        var session = await _context.LectureSessions.FirstOrDefaultAsync(x => x.Id == groups.FirstOrDefault().CurrentSession);
                    

        if (!groups.Any())
        {
            throw new Exception("No groups found for the given lecturer");
        }

        var groupWithSyllabusDTOs = groups.Select(group =>
        {
            bool isOnline = group.GroupLectureSessions.FirstOrDefault()?.LectureSession.IsOnline ?? false;

            return new GroupWithSyllabusDTO
            {
                CompanyName = group.CompanyName,
                GroupName = group.GroupName,
                Grade = group.Grade,
                Status = group.Status,
                IsOnline = isOnline,
                SessionsAmount = group.SessionsAmount,
                StartDate = group.StartDate,
                EndDate = group.EndDate,
                CurrentSession = group.CurrentSession,
                HEX = group.HEX,
                GroupType = group.GroupType,
                Auditorium = group.GroupLectureSessions.FirstOrDefault()?.LectureSession.Auditorium,
                SyllabusTopics = group.SyllabusTopics.Select(t => new SyllabusDTO
                {
                    Id = t.Id,
                    Topic = t.Title,
                    Details = t.Details,
                    Order = t.Order,
                }).ToList(),
                HasSyllabus = group.SyllabusTopics.Any()
            };
        }).ToList();

        return groupWithSyllabusDTOs;
    }

    public async Task<IEnumerable<LecturerGroupDetailsDTO>> GetGroupDetailsWithSessionsForLecturer(int lecturerId)
    {
        // Fetch all groups for the given lecturerId
        var groups = await _context.LecturerGroups
            .Where(g => g.LecturerId == lecturerId)
            .Include(g => g.GroupLectureSessions)
                .ThenInclude(gl => gl.LectureSession)
            .ToListAsync();

        if (!groups.Any())
        {
            throw new Exception("No groups found for the given lecturer");
        }

        var groupDetailsList = groups.Select(group => new LecturerGroupDetailsDTO
        {
            Id = group.Id,
            CompanyName = group.CompanyName,
            GroupName = group.GroupName,
            Grade = group.Grade,
            Status = group.Status,
            StartDate = group.StartDate,
            EndDate = group.EndDate,
            CurrentSession = group.CurrentSession,
            SessionsAmount = group.SessionsAmount,
            CurrentSyllabusTopicId = group.CurrentSyllabusTopicId,
            GroupType = group.GroupType,
            HEXColor = group.HEX,
            Sessions = group.GroupLectureSessions.Select(gl => new LectureSessionGetDTO
            {
                Id = gl.LectureSession.Id,
                Day = gl.LectureSession.Day,
                Time = gl.LectureSession.Time,
                Auditorium = gl.LectureSession.Auditorium,
                IsOnline = gl.LectureSession.IsOnline,
                // take only the first 2 days
                // Probably should be done in the frontend
                // pagination will be needed
            }).ToList(),
        }).ToList();

        return groupDetailsList;
    }

    public async Task<IEnumerable<LectureGroupDTO>> GetAllGroupsForLecturerAsync(int lecturerId)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == lecturerId);
        if (lecturer == null)
        {
            throw new Exception("Lecturer not found");
        }

        var groups = _context.LecturerGroups.Where(x => x.LecturerId == lecturerId)
            .Include(x => x.GroupLectureSessions)
            .ThenInclude(x => x.LectureSession);


        if (!await groups.AnyAsync())
        {
            throw new Exception("No groups found for the given lecturer");
        }


        var lectureGroupDTOs = await groups.Select(group => new LectureGroupDTO
        {
            CompanyName = group.CompanyName,
            GroupName = group.GroupName,
            Grade = group.Grade,
            Status = group.Status,
            SessionsAmount = group.SessionsAmount,
            StartDate = group.StartDate,
            PerWeek = group.PerWeek,
            IsOnline = group.IsOnline,
            HEXColor = group.HEX,
            GroupType = group.GroupType,
            Auditorium = group.GroupLectureSessions.FirstOrDefault().LectureSession.Auditorium,
            Time = group.GroupLectureSessions.FirstOrDefault().LectureSession.Time,
            Days = String.Join(", ", group.GroupLectureSessions
                              .GroupBy(s => s.LectureSession.Day)
                              .OrderByDescending(g => g.Count())
                              .Take(2)
                              .Select(g => g.Key.ToString())),

        }).ToListAsync();

        return lectureGroupDTOs;
    }

}
