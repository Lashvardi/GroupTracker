using GroupTracker.DTOs.Groups;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Group;

public interface IGroupService
{
    Task<LecturerGroup> CreateGroup(CompleteGroupInput completeGroupInput);

}
