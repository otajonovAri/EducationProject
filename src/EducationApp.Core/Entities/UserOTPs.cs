using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class UserOTPs : BaseEntity
{
    public int UserId { get; set; }
    public string Code { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiredAt { get; set; }

    public User User { get; set; } = null!;

    public ICollection<UserOTPs> OtpCodes { get; set; } = new List<UserOTPs>();
}