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


    // Images related stuff
    Task<IFormFile> UploadProfilePictureAsync(int lecturerId, IFormFile file);
    Task<string> GetProfilePictureAsync(int lecturerId);
    void DeleteProfilePicture(int lecturerId);

    Task<IFormFile> UploadBannerPictureAsync(int lecturerId, IFormFile file);
    Task<string> GetbannerImageAsync(int lecturerId);
    void DeleteBannerPicture(int lecturerId);


    // Socials related stuff
    Task<LecturerSocialsDTO> AddSocialsAsync(int lecturerId, LecturerSocialsDTO socials);
    Task<LecturerSocialsDTO> GetSocialsAsync(int lecturerId);

    // has filled out socials
    Task<bool> HasFilledOutSocialsAsync(int lecturerId);

    // Delete Lecturer
    Task DeleteLecturerAsync(int lecturerId);

}
