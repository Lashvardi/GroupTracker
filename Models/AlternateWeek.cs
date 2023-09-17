﻿using System.Text.Json.Serialization;

namespace GroupTracker.Models;

// THIS IS FOR THE ALTERNATE LECTURE WEEKS FOR ME TO TRACK 
public class AlternateWeek
{
    public int Id { get; set; }
    public string WeekNumber { get; set; }
    public int LectureSessionId { get; set; }

    // Navigation Properties

    [JsonIgnore]
    public LectureSession LectureSession { get; set; }
}
