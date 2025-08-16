using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.PaymentDto;

public class PaymentUpdateDto
{
    public decimal? Amount { get; set; }
    public DateTime? PaymentDate { get; set; }
    public PaymentType? PaymentType { get; set; }
    public string? PaymentImgUrl { get; set; }
    public string? Description { get; set; }
}