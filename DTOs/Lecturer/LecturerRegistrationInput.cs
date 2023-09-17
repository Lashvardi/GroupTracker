﻿using System.ComponentModel.DataAnnotations;

namespace GroupTracker.DTOs.Lecturer;

public class LecturerRegistrationInput
{

    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
