﻿namespace GroupTracker.DTOs.Groups;

public class LectureSessionDTO
{
    public string Day { get; set; }
    public string Time { get; set; }
    public string Auditorium { get; set; }
    public bool IsOnline { get; set; } = false;
    public bool IsAlternate { get; set; } = false;
}