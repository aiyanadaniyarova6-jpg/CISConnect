using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CISConnect.Services;

public interface IEmailService
{
    Task SendContactFormAsync(string fromName, string fromEmail, string subject, string message);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendContactFormAsync(string fromName, string fromEmail, string subject, string message)
    {
        var smtpHost = _config["Email:SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = _config.GetValue<int>("Email:SmtpPort", 587);
        var smtpUser = _config["Email:SmtpUser"] ?? string.Empty;
        var smtpPass = _config["Email:SmtpPass"] ?? string.Empty;
        var toEmail  = _config["Email:ToAddress"] ?? smtpUser;

        _logger.LogInformation("Sending email: host={Host} port={Port} user={User} to={To}",
            smtpHost, smtpPort, smtpUser, toEmail);

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("CIS Connect", smtpUser));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.ReplyTo.Add(new MailboxAddress(fromName, fromEmail));
        email.Subject = $"[CIS Connect] {subject}";
        email.Body = new TextPart("plain")
        {
            Text = $"From: {fromName} <{fromEmail}>\n\n{message}"
        };

        using var smtp = new SmtpClient();
        smtp.Timeout = 15000; // 15s — fail fast instead of hanging if the port is blocked
        await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(smtpUser, smtpPass);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

        _logger.LogInformation("Email sent successfully to {To}", toEmail);
    }
}
