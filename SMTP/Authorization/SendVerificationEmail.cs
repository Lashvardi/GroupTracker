using GroupTracker.ENV;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace SMTP.Authorization.EmailVerification
{
    public class EmailVerification
    {
        private readonly string _sendGridApiKey;

        public EmailVerification(IOptions<AppSettings> settings)
        {
            _sendGridApiKey = settings.Value.SendGrid.ApiKey;
        }

        public async Task VerifyEmail( string recipientEmail, string verificationCode)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("lashakakashvili114@gmail.com", "Group Tracker Support");
            var to = new EmailAddress(recipientEmail);
            var subject = "Your Verification Code";

            var plainTextContent = $"Your verification code is {verificationCode}.";
            var htmlContent = $@"
                <html>
                <body>
                    <h1>Welcome to Tracker!</h1>
                    <p>We're excited to have you on board. To complete your registration, please enter the following verification code:</p>
                    <h2 style='color: #3498db;'>{verificationCode}</h2>
                    <p>If you didn't request this, you can safely ignore this email.</p>
                </body>
                </html>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
