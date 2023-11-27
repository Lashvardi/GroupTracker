using GroupTracker.data;
using GroupTracker.DTOs.Groups;
using GroupTracker.Enums;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Group;

namespace GroupTracker.Services.Implementation.Group;

public class LecturerSessionService : ILectureSessionService
{
    private readonly DataContext _context;

    public LecturerSessionService(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<LectureSession> CreateLectureSession(LectureSessionDTO lectureSessionDTO, Weekday day)
    {
        var newSession = new LectureSession
        {
            Day = day,
            Time = lectureSessionDTO.Time,
            Auditorium = lectureSessionDTO.Auditorium,
            IsOnline = lectureSessionDTO.IsOnline,
        };

        _context.LectureSessions.Add(newSession);

        return newSession;
    }

}
