using GroupTracker.ENV;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using GroupTracker.Models;
using System.Text;

namespace GroupTracker.SMTP.Notification;

public class TodaysLectures
{
    private readonly string _sendGridApiKey;

    public TodaysLectures(IOptions<AppSettings> settings)
    {
        _sendGridApiKey = settings.Value.SendGrid.ApiKey;
    }

    public async Task GetTodaysLectures(string recipientEmail, List<LectureSession> lectureSessions)
    {
        var client = new SendGridClient(_sendGridApiKey);
        var from = new EmailAddress("lashakakashvili114@gmail.com", "Today's Lectures");
        var to = new EmailAddress(recipientEmail);
        var subject = "Your Lecture Schedule for Today";

        // Build the HTML content with lecture session details
        var htmlContent = new StringBuilder();
        htmlContent.Append("<h1>Today's Lectures:</h1>");

        foreach (var session in lectureSessions)
        {
            htmlContent.Append("<div style='border: 1px solid #ddd; margin: 10px 0; padding: 10px;'>");
            htmlContent.Append($"<p><strong>Time:</strong> {session.Time}</p>");
            htmlContent.Append($"<p><strong>Auditorium:</strong> {session.Auditorium}</p>");
            htmlContent.Append($"<p><strong>Online:</strong> {(session.IsOnline ? "Yes" : "No")}</p>");
            htmlContent.Append("</div>");
        }

        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent.ToString());
        var response = await client.SendEmailAsync(msg);
    }

}
