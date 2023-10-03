using GroupTracker.DTOs.Groups;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupTracker.Services.Implementation.Group;

public class AlternateWeekService : IAlternateWeekService
{
    public Task<List<AlternateWeek>> CreateAlternateWeeks(bool doIstart, int totalWeeks)
    {
        List<AlternateWeek> newAlternateWeeks = new List<AlternateWeek>();

        // Decide the starting week based on the 'startFromFirstWeek' flag
        var startWeek = 0;
        if (doIstart)
        {
            startWeek = 1;
        }
        else
        {
            startWeek = 2;
        }

        // Generate alternate weeks based on the starting week
        for (int i = startWeek; i <= totalWeeks; i += 2)
        {
            newAlternateWeeks.Add(new AlternateWeek
            {
                WeekNumber = i
            });
        }

        return Task.FromResult(newAlternateWeeks);
    }
}
