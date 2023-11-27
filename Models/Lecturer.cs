using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


    public string ImageUrl { get; set; }

    public string bannerImageUrl { get; set; }

    [NotMapped]
    public IFormFile BannerImageFile { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }

    // Social Media Links
    public string FacebookLink { get; set; }
    public string TwitterLink { get; set; }
    public string InstagramLink { get; set; }
    public string LinkedInLink { get; set; }
    public string YouTubeLink { get; set; }
    public string PersonalWebsiteLink { get; set; }

    // Navigation Properties
    public ICollection<LecturerGroup> LecturerGroups { get; set; }
}