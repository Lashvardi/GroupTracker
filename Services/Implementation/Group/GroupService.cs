using GroupTracker.data;
using GroupTracker.DTOs.Groups;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Group;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.Services.Implementation.Group;

public class GroupService : IGroupService
{

    private readonly DataContext _context;

    public GroupService(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<LecturerGroup> CreateGroup(CompleteGroupInput completeGroupInput)
    {
        var newGroup = new LecturerGroup
        {
            CompanyName = completeGroupInput.Group.CompanyName,
            GroupName = completeGroupInput.Group.GroupName,
            Grade = completeGroupInput.Group.Grade,
            WeeksAmount = completeGroupInput.Group.WeeksAmount,
            StartDate = completeGroupInput.Group.StartDate,
            DoIStart = completeGroupInput.Group.DoIStart,
        };
        await _context.LecturerGroups.AddAsync(newGroup);
        return newGroup;
    }


}
