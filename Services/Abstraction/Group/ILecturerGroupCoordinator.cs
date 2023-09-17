using GroupTracker.DTOs.Groups;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Group;

public interface ILecturerGroupCoordinator
{
    Task<LecturerGroup> CreateAndAssignGroup(int lecturerId, CompleteGroupInput completeGroupInput);
    Task<LecturerGroup> DeleteGroup(int groupId);

    //Give me method where i can change the topic of a group i want to provide topic id and group id
    Task<LecturerGroup> ChangeGroupTopic(int groupId, int topicId);
}
