using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.EmailSendServices;

public class OtpService : IOtpService
{
    private readonly EduDbContext _context;
    private readonly IEmailService _emailService;

    public OtpService(EduDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<string> GenerateAndSaveOtpAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        var otpCode = new Random().Next(100000, 999999).ToString();

        var otp = new UserOTPs
        {
            UserId = userId,
            Code = otpCode,
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddMinutes(5)
        };

        await _context.UserOTPs.AddAsync(otp);
        await _context.SaveChangesAsync();

        await _emailService.SendOtpAsync(user.Email, otpCode);
        return otpCode;
    }

    public async Task<UserOTPs?> GetLatestOtpAsync(int userId, string code)
    {
        var otp = await _context.UserOTPs
            .Where(o => o.UserId == userId && o.Code == code)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync();

        return otp;
    }
}