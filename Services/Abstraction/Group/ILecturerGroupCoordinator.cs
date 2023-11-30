using GroupTracker.DTOs.Groups;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.DTOs.LecturerSessions;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Group;

public interface ILecturerGroupCoordinator
{
    Task<LecturerGroup> CreateAndAssignGroup(int lecturerId, CompleteGroupInput completeGroupInput);
    Task<LecturerGroup> DeleteGroup(int groupId);

    Task<LecturerGroup> ChangeGroupTopic(int groupId, int topicId);

    Task<List<GroupWithSyllabusDTO>> GetAllGroupsWithSyllabusForLecturer(int lecturerId);
    Task<IEnumerable<LecturerGroupDetailsDTO>> GetGroupDetailsWithSessionsForLecturer( int lecturerId);

    //Get All Groups without Syllabus and Sessions
    Task<IEnumerable<LectureGroupDTO>> GetAllGroupsForLecturerAsync(int lecturerId);

}
