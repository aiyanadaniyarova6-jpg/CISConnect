using System.Net.Http.Headers;
using System.Net.Http.Json;
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
    private readonly IHttpClientFactory _httpFactory;

    public EmailService(IConfiguration config, ILogger<EmailService> logger, IHttpClientFactory httpFactory)
    {
        _config = config;
        _logger = logger;
        _httpFactory = httpFactory;
    }

    public async Task SendContactFormAsync(string fromName, string fromEmail, string subject, string message)
    {
        // Prefer Resend (HTTP API, port 443) — works on hosts that block SMTP. Falls back to SMTP locally.
        var resendKey = _config["Resend:ApiKey"];
        if (!string.IsNullOrWhiteSpace(resendKey))
        {
            await SendViaResendAsync(resendKey, fromName, fromEmail, subject, message);
            return;
        }

        await SendViaSmtpAsync(fromName, fromEmail, subject, message);
    }

    // Sends via Resend's HTTP API — used in production (Railway blocks SMTP ports).
    private async Task SendViaResendAsync(string apiKey, string fromName, string fromEmail, string subject, string message)
    {
        var toEmail = _config["Email:ToAddress"] ?? _config["Email:SmtpUser"] ?? string.Empty;
        var fromAddress = _config["Resend:FromAddress"] ?? "onboarding@resend.dev";

        _logger.LogInformation("Sending email via Resend to {To}", toEmail);

        var payload = new
        {
            from = $"CIS Connect <{fromAddress}>",
            to = new[] { toEmail },
            reply_to = fromEmail,
            subject = $"[CIS Connect] {subject}",
            text = $"From: {fromName} <{fromEmail}>\n\n{message}"
        };

        var http = _httpFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        request.Content = JsonContent.Create(payload);

        var response = await http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new Exception($"Resend API error {(int)response.StatusCode}: {body}");
        }

        _logger.LogInformation("Email sent via Resend to {To}", toEmail);
    }

    // Sends via Gmail SMTP — used locally where port 587 is open.
    private async Task SendViaSmtpAsync(string fromName, string fromEmail, string subject, string message)
    {
        var smtpHost = _config["Email:SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = _config.GetValue<int>("Email:SmtpPort", 587);
        var smtpUser = _config["Email:SmtpUser"] ?? string.Empty;
        var smtpPass = _config["Email:SmtpPass"] ?? string.Empty;
        var toEmail  = _config["Email:ToAddress"] ?? smtpUser;

        _logger.LogInformation("Sending email via SMTP: host={Host} port={Port} user={User} to={To}",
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
