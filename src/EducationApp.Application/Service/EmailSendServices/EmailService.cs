using EducationApp.Application.Common;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EducationApp.Application.Service.EmailSendServices;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _config;

    public EmailService(IOptions<EmailConfiguration> config)
    {
        _config = config.Value;
    }
    public async Task<bool> SendOtpAsync(string toEmail, string otp)
    {
        try
        {
            using var client = new SmtpClient(_config.SmtpServer, _config.Port)
            {
                EnableSsl = _config.EnableSsl,
                Credentials = new NetworkCredential(_config.Username, _config.Password)
            };

            var message = new MailMessage
            {
                From = new MailAddress(_config.DefaultFromEmail, _config.DefaultFromName),
                Subject = "SecureLoginApp: OTP Verification Code",
                Body = GenerateBody(otp),
                IsBodyHtml = true
            };

            message.To.Add(toEmail);
            await client.SendMailAsync(message);
            return true;
        }
        catch (SmtpException smtpEx)
        {
            // SMTP ga oid xatolarni qayd qiling
            Console.WriteLine($"Elektron pochta yuborishda SMTP xatosi {toEmail} ga: {smtpEx.StatusCode} - {smtpEx.Message}");
            if (smtpEx.InnerException != null)
            {
                Console.WriteLine($"Ichki istisno: {smtpEx.InnerException.Message}");
            }
            return false;
        }
        catch (Exception ex)
        {
            // Umumiy istisnolarni qayd qiling
            Console.WriteLine($"Elektron pochta yuborishda umumiy xato {toEmail} ga: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Ichki istisno: {ex.InnerException.Message}");
            }
            return false;
        }
    }

    private string GenerateBody(string otp)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<html><body style='font-family:sans-serif;'>");
        sb.AppendLine("<h3>Welcome to SecureLoginApp!</h3>");
        sb.AppendLine("<p>Your one-time verification code is:</p>");
        sb.AppendLine($"<div style='font-size: 24px; font-weight: bold; margin: 20px 0;'>{otp}</div>");
        sb.AppendLine("<p>Please do not share this code with anyone. It will expire in 5 minutes.</p>");
        sb.AppendLine("<p>If you did not request this, please ignore.</p>");
        sb.AppendLine("<br/><small>&copy; 2025 SecureLoginApp</small>");
        sb.AppendLine("</body></html>");
        return sb.ToString();
    }
}