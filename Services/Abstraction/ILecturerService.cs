using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction;

public interface ILecturerService
{
    Task<LecturerRegistrationResponse> RegisterLecturer(LecturerRegistrationInput lecturer);
    Task<string> LoginLecturer(string email, string password);


    Task<LecturerDTO> GetLecturerInfoAsync(int lecturerId);
}
