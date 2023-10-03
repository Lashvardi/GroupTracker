using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction;

public interface ILecturerService
{
    Task<LecturerRegistrationResponse> RegisterLecturer(LecturerRegistrationInput lecturer);
    Task<string> LoginLecturer(string email, string password);
    Task<string> SendVerificationEmail(string email);
    Task<string> VerifyLecturer(string email, string token);

    Task<List<LecturerSubjects>> GetLecturerSubjects(int id);
    Task<List<LecturerCompanies>> GetLecturerCompanies(int id);



    Task<LecturerDTO> GetLecturerInfoAsync(int lecturerId);

}
