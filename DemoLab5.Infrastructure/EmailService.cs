using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace DemoLab5.Infrastructure;

public interface IEmailService
{
    Task SendEmailAsync(List<string> to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    public async Task SendEmailAsync(List<string> toEmails, string subject, string body)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Inmind University", _smtpUsername));

            // Add all student emails in BCC to prevent revealing recipients
            foreach (var emailAddress in toEmails)
            {
                email.Bcc.Add(new MailboxAddress("", emailAddress));
            }

            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

            Console.WriteLine($"Email sent successfully to {toEmails.Count} students.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}