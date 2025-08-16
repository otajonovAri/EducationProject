using EducationApp.Core.Entities;

namespace EducationApp.Application.Service.EmailSendServices;

public interface IOtpService
{
    Task<string> GenerateAndSaveOtpAsync(int userId);
    Task<UserOTPs?> GetLatestOtpAsync(int userId, string code);
}