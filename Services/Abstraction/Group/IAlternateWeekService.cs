using GroupTracker.DTOs.Groups;
using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction.Group;

public interface IAlternateWeekService
{
    Task<List<AlternateWeek>> CreateAlternateWeeks(List<AlternateWeekDTO> alternateWeekDTOs);
}