using System.ComponentModel.DataAnnotations;

namespace GroupTracker.Models;

public class Lecturer
{
    public int Id { get; set; }



    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }


    // Navigation Properties
    public ICollection<LecturerGroup> LecturerGroup { get; set; }
}
