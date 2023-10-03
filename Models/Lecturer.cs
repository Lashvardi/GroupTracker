using System.ComponentModel.DataAnnotations;

namespace GroupTracker.Models;

public class Lecturer
{
    // Primary Key
    public int Id { get; set; }

    // Basic Information
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Employment Information CSV
    public string Companies { get; set; }

    // Academic Information CSV
    public string Subjects { get; set; }

    // Authentication and Authorization
    [Required]
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    // Verification Information
    public bool IsVerified { get; set; }
    public string VerificationCode { get; set; }

    // Navigation Properties
    public ICollection<LecturerGroup> LecturerGroups { get; set; }
}