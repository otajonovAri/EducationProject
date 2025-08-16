using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.PaymentDto;

public class PaymentResponseDto
{
    /* public int Id { get; set; }
     public int StudentId { get; set; }
     public string? StudentName { get; set; }
     public decimal Amount { get; set; }
     public DateTime PaymentDate { get; set; }
     public PaymentType PaymentType { get; set; }
     public string? Description { get; set; }
     public IEnumerable<PaymentDocumentDto>? Documents { get; set; }*/

    public int Id { get; set; }
    public int StudentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? PaymentImgUrl { get; set; }
    public string? Description { get; set; }
    public IEnumerable<PaymentDocumentDto>? Documents { get; set; }
}