using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class PaymentDocument : BaseEntity
{
    public string? FileUrl { get; set; }
    public int PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;
}