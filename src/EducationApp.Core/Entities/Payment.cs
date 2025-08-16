using EducationApp.Core.Common;
using EducationApp.Core.Enums;

namespace EducationApp.Core.Entities;

public class Payment : BaseEntity
{
    // relationships
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PaymentImgUrl { get; set; }
    public string? Description { get; set; }

    // navigation properties
    public ICollection<PaymentDocument> PaymentDocuments { get; set; } = new HashSet<PaymentDocument>();
}
