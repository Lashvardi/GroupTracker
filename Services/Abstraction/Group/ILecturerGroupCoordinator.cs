using GroupTracker.DTOs.Groups;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Group;

public interface ILecturerGroupCoordinator
{
    Task<LecturerGroup> CreateAndAssignGroup(int lecturerId, CompleteGroupInput completeGroupInput);
}
