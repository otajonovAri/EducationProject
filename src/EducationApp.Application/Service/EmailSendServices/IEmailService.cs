namespace EducationApp.Application.Service.EmailSendServices;

public interface IEmailService
{
    Task<bool> SendOtpAsync(string toEmail, string otp);
}