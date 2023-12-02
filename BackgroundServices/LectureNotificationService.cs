using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GroupTracker.SMTP.Notification;
using GroupTracker.data;
using GroupTracker.Enums;
using GroupTracker.Services;

public class LectureNotificationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public LectureNotificationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var todaysLectures = scope.ServiceProvider.GetRequiredService<TodaysLectures>();
                var todaysLecturesSMS = scope.ServiceProvider.GetRequiredService<TodaysLecturesSMS>();
                var lectureScheduleService = scope.ServiceProvider.GetRequiredService<LectureScheduleService>();
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                var users = context.Lecturers.ToList();
                var today = DateTime.Now.DayOfWeek;
                var weekday = ConvertDayOfWeek(today);

                foreach (var user in users)
                {
                    var lectureSessions = await lectureScheduleService.GetLecturesForDay(user.Id, weekday);
                    await todaysLectures.GetTodaysLectures(user.Email, lectureSessions);

                    // Assuming users have a PhoneNumber property
                    if (!string.IsNullOrEmpty("+995551783732"))
                    {
                        await todaysLecturesSMS.SendTodaysLecturesSMS("+995551783732", lectureSessions);
                    }
                }
            }

        }

        Weekday ConvertDayOfWeek(DayOfWeek day)
        {
            // Adjust for Monday as 0 and Sunday as 6
            return day switch
            {
                DayOfWeek.Sunday => Weekday.Sunday, // Sunday is 6
                DayOfWeek.Monday => Weekday.Monday, // Monday is 0
                DayOfWeek.Tuesday => Weekday.Tuesday, // Tuesday is 1
                DayOfWeek.Wednesday => Weekday.Wednesday, // Wednesday is 2
                DayOfWeek.Thursday => Weekday.Thursday, // Thursday is 3
                DayOfWeek.Friday => Weekday.Friday, // Friday is 4
                DayOfWeek.Saturday => Weekday.Saturday, // Saturday is 5
                _ => throw new ArgumentOutOfRangeException(nameof(day), $"Not expected day value: {day}")
            };
        }

    }
}
