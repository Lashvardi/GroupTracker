﻿using GroupTracker.DTOs.Groups;

namespace GroupTracker.DTOs.Lecturer;

public class LecturerDTO
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Companies { get; set; }
    public ICollection<LecturerGroupDTO> Groups { get; set; }

}
