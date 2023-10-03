namespace GroupTracker.ENV;

public class AppSettings
{
    public SendGridSettings SendGrid { get; set; }
    public JwtSettings Jwt { get; set; }
}

public class SendGridSettings
{
    public string ApiKey { get; set; }
}

public class JwtSettings
{
    public string SecurityKey { get; set; }
}
