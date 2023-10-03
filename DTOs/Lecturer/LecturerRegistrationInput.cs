using System.ComponentModel.DataAnnotations;

namespace GroupTracker.DTOs.Lecturer;

public class LecturerRegistrationInput
{

    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    // comma separated list of companies (e.g. "Google, Microsoft, Apple")
    public string Companies { get; set; }


    // CSV of subjects (e.g. "Maths, English, Science")
    public string Subjects { get; set; }
}
