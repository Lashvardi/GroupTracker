using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using GroupTracker.ENV;
using System.Text;
using System.Threading.Tasks;
using GroupTracker.Models;

namespace GroupTracker.SMTP.Notification;

public class TodaysLecturesSMS
{
    private readonly string _twilioAccountSid;
    private readonly string _twilioAuthToken;
    private readonly string _twilioPhoneNumber;

    public TodaysLecturesSMS(IOptions<AppSettings> settings)
    {
        _twilioAccountSid = settings.Value.Twilio.AccountSid;
        _twilioAuthToken = settings.Value.Twilio.AuthToken;
        _twilioPhoneNumber = settings.Value.Twilio.PhoneNumber;
    }

    public async Task SendTodaysLecturesSMS(string recipientPhoneNumber, List<LectureSession> lectureSessions)
    {
        TwilioClient.Init(_twilioAccountSid, _twilioAuthToken);

        var messageContent = new StringBuilder();
        messageContent.Append("Today's Lectures:\n");

        foreach (var session in lectureSessions)
        {
            messageContent.AppendLine($"Time: {session.Time}");
            messageContent.AppendLine($"Auditorium: {session.Auditorium}");
            messageContent.AppendLine($"Online: {(session.IsOnline ? "Yes" : "No")}\n");
        }

        var message = await MessageResource.CreateAsync(
            body: messageContent.ToString(),
            from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
            to: new Twilio.Types.PhoneNumber("+995591299899")
        );
    }
}
