using GroupTracker.Models;
using GroupTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GroupTracker.data;

namespace GroupTracker.Services
{
    public class LectureScheduleService
    {
        private readonly DataContext _context;

        public LectureScheduleService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<LectureSession>> GetLecturesForToday(int lecturerId)
        {
            var today = DateTime.Now.DayOfWeek;
            var weekday = ConvertDayOfWeek(today);


            var lecturesToday = await _context.LecturerGroups
                .Where(lg => lg.LecturerId == lecturerId)
                .SelectMany(lg => lg.GroupLectureSessions)
                .Select(gls => gls.LectureSession)
                .Where(ls => ls.Day == weekday)
                .Distinct() // To avoid duplicate LectureSessions
                .ToListAsync();

            if (0 == lecturerId)
            {
                throw new Exception("Lecturer not found");
            }



            return lecturesToday;
        }

        public async Task<List<LectureSession>> GetLecturesForTomorrow(int lecturerId)
        {
            var today = DateTime.Now.DayOfWeek;
            var tomorrow = ConvertDayOfWeek(today + 1);

            var lecturesTomorrow = await _context.LecturerGroups
                .Where(lg => lg.LecturerId == lecturerId)
                .SelectMany(lg => lg.GroupLectureSessions)
                .Select(gls => gls.LectureSession)
                .Where(ls => ls.Day == tomorrow)
                .Distinct() // To avoid duplicate LectureSessions
                .ToListAsync();

            if (0 == lecturerId)
            {
                throw new Exception("Lecturer not found");
            }

            return lecturesTomorrow;
        }

        public async Task<List<LectureSession>> GetLecturesForThisWeek(int lecturerId)
        {
            var today = DateTime.Now.DayOfWeek;
            var weekday = ConvertDayOfWeek(today);

            var lecturesThisWeek = await _context.LecturerGroups
                .Where(lg => lg.LecturerId == lecturerId)
                .SelectMany(lg => lg.GroupLectureSessions)
                .Select(gls => gls.LectureSession)
                .Where(ls => ls.Day >= weekday)
                .Distinct() // To avoid duplicate LectureSessions
                .ToListAsync();

            if (0 == lecturerId)
            {
                throw new Exception("Lecturer not found");
            }

            return lecturesThisWeek;
        }

        public async Task<List<LectureSession>> GetLecturesForNextWeek(int lecturerId)
        {
            var today = DateTime.Now.DayOfWeek;
            var weekday = ConvertDayOfWeek(today);

            var lecturesNextWeek = await _context.LecturerGroups
                .Where(lg => lg.LecturerId == lecturerId)
                .SelectMany(lg => lg.GroupLectureSessions)
                .Select(gls => gls.LectureSession)
                .Where(ls => ls.Day >= weekday + 7)
                .Distinct() // To avoid duplicate LectureSessions
                .ToListAsync();

            if (0 == lecturerId)
            {
                throw new Exception("Lecturer not found");
            }

            return lecturesNextWeek;
        }

        public async Task<List<LectureSession>> GetLecturesForThisMonth(int lecturerId)
        {
            var today = DateTime.Now.DayOfWeek;
            var weekday = ConvertDayOfWeek(today);

            var lecturesThisMonth = await _context.LecturerGroups
                .Where(lg => lg.LecturerId == lecturerId)
                .SelectMany(lg => lg.GroupLectureSessions)
                .Select(gls => gls.LectureSession)
                .Where(ls => ls.Day >= weekday + 30)
                .Distinct() // To avoid duplicate LectureSessions
                .ToListAsync();

            if (0 == lecturerId)
            {
                throw new Exception("Lecturer not found");
            }

            return lecturesThisMonth;
        }

        public async Task<List<LectureSession>> GetLecturesForDay(int lecturerId, Weekday day)
        {
            if (lecturerId == 0)
            {
                throw new ArgumentException("Lecturer not found", nameof(lecturerId));
            }

            var lecturesForDay = await _context.LecturerGroups
                .Where(lg => lg.LecturerId == lecturerId)
                .SelectMany(lg => lg.GroupLectureSessions)
                .Select(gls => gls.LectureSession)
                .Where(ls => ls.Day == day)
                .GroupBy(ls =>  ls.Time) // Groups by StartTime
                .Select(group => group.First()) // Selects the first item from each group
                .ToListAsync();

            return lecturesForDay;
        }



        private Weekday ConvertDayOfWeek(DayOfWeek day)
        {
            // Convert System.DayOfWeek to GroupTracker.Enums.Weekday
            // Adjusted for Monday as 0 and Sunday as 6
            return day switch
            {
                DayOfWeek.Sunday => Weekday.Sunday,
                DayOfWeek.Monday => Weekday.Monday,
                DayOfWeek.Tuesday => Weekday.Tuesday,
                DayOfWeek.Wednesday => Weekday.Wednesday,
                DayOfWeek.Thursday => Weekday.Thursday,
                DayOfWeek.Friday => Weekday.Friday,
                DayOfWeek.Saturday => Weekday.Saturday,
                _ => throw new ArgumentOutOfRangeException(nameof(day), $"Not expected day value: {day}")
            };
        }

    }
}
