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

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendContactFormAsync(string fromName, string fromEmail, string subject, string message)
    {
        var smtpHost = _config["Email:SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = _config.GetValue<int>("Email:SmtpPort", 587);
        var smtpUser = _config["Email:SmtpUser"] ?? string.Empty;
        var smtpPass = _config["Email:SmtpPass"] ?? string.Empty;
        var toEmail  = _config["Email:ToAddress"] ?? smtpUser;

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
        await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(smtpUser, smtpPass);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
