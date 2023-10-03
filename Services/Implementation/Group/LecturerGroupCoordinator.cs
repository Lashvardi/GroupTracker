﻿using GroupTracker.data;
using GroupTracker.DTOs.GroupLectureSession;
using GroupTracker.DTOs.Groups;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.DTOs.Syllabus;
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

        // Create sessions based on the number of weeks and alternate flag
        List<LectureSession> allSessions = new List<LectureSession>();
        List<GroupLectureSession> allGroupLectureSessions = new List<GroupLectureSession>();

        int totalWeeks = completeGroupInput.Group.WeeksAmount;
        List<AlternateWeek> alternateWeeks = null;

        if (completeGroupInput.Session.IsAlternate)
        {
            bool doIStart = completeGroupInput.Session.StartFromFirstWeek;
            alternateWeeks = await _alternateWeekService.CreateAlternateWeeks(doIStart, totalWeeks);
        }

        int sessionCount;
        if (completeGroupInput.Session.IsAlternate)
        {
            sessionCount = alternateWeeks.Count;
        }
        else
        {
            sessionCount = totalWeeks;
        }

        for (int i = 0; i < sessionCount; i++)
        {
            var newSession = await _lectureSessionService.CreateLectureSession(completeGroupInput.Session);
            allSessions.Add(newSession);

            var newGroupLectureSession = new GroupLectureSession
            {
                Group = newGroup,
                LectureSession = newSession
            };

            allGroupLectureSessions.Add(newGroupLectureSession);
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


    public async Task<List<GroupWithSyllabusDTO>> GetAllGroupsWithSyllabusForLecturer(int lecturerId)
    {
        var groups = await _context.LecturerGroups
                    .Where(x => x.LecturerId == lecturerId)
                    .Include(x => x.SyllabusTopics)
                    .Include(x => x.GroupLectureSessions)
                        .ThenInclude(gl => gl.LectureSession)
                    .ToListAsync();

        if (!groups.Any())
        {
            throw new Exception("No groups found for the given lecturer");
        }

        var groupWithSyllabusDTOs = groups.Select(group =>
        {
            bool isOnline = group.GroupLectureSessions.FirstOrDefault()?.LectureSession.IsOnline ?? false;
            bool isAlternate = group.GroupLectureSessions.FirstOrDefault()?.LectureSession.IsAlternate ?? false;

            return new GroupWithSyllabusDTO
            {
                CompanyName = group.CompanyName,
                GroupName = group.GroupName,
                Grade = group.Grade,
                Status = group.Status,
                IsOnline = isOnline,
                IsAlternate = isAlternate,
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


}
