using GroupTracker.data;
using GroupTracker.Enums;
using GroupTracker.Models;
using GroupTracker.Services;
using GroupTracker.SMTP.Notification;
using Microsoft.AspNetCore.Mvc;

namespace GroupTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : Controller
    {
        private readonly LectureScheduleService _lectureScheduleService;
        private readonly TodaysLectures _todaysLectures;
        private readonly DataContext _context;

        public ScheduleController(LectureScheduleService lectureScheduleService, TodaysLectures todaysLectures, DataContext context)
        {
            _lectureScheduleService = lectureScheduleService;
            _todaysLectures = todaysLectures;
            _context = context;
        }

        [HttpGet("get-lecture-schedule/{lecturerId}")]
        public async Task<IActionResult> GetLectureSchedule(int lecturerId)
        {
            try
            {
                var result = await _lectureScheduleService.GetLecturesForToday(lecturerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-lecture-schedule-for-week/{lecturerId}")]
        public async Task<IActionResult> GetLectureScheduleForWeek(int lecturerId)
        {
            try
            {
                var result = await _lectureScheduleService.GetLecturesForNextWeek(lecturerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-lecture-schedule-for-month/{lecturerId}")]
        public async Task<IActionResult> GetLectureScheduleForMonth(int lecturerId)
        {
            try
            {
                if(lecturerId == 0)
                {
                    return BadRequest("Lecturer id is not valid");
                }
                var result = await _lectureScheduleService.GetLecturesForThisMonth(lecturerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-lecture-schedule-for-certain-day/{lecturerId}/{day}")]
        public async Task<IActionResult> GetLectureScheduleForCertainDay(int lecturerId, Weekday day)
        {
            try
            {
                var lectureSessions = await _lectureScheduleService.GetLecturesForDay(lecturerId, day);
                return Ok(lectureSessions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
