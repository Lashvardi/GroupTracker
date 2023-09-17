using GroupTracker.DTOs.Groups;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Group;

namespace GroupTracker.Services.Implementation.Group;

public class AlternateWeekService : IAlternateWeekService
{
    public Task<List<AlternateWeek>> CreateAlternateWeeks(List<AlternateWeekDTO> alternateWeekDTOs)
    {
        List<AlternateWeek> newAlternateWeeks = new List<AlternateWeek>();
        foreach (var weekDto in alternateWeekDTOs)
        {
            newAlternateWeeks.Add(new AlternateWeek
            {
                WeekNumber = weekDto.WeekNumbers
            });
        }
        return Task.FromResult(newAlternateWeeks);
    }
}