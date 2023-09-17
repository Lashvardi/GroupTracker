using GroupTracker.DTOs.Groups;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Group;

public interface ILectureSessionService
{
    Task<LectureSession> CreateLectureSession(LectureSessionDTO lectureSessionDTO);
}
