namespace EducationApp.Application.DTOs.PaymentDocumentDto;

public class PaymentDocumentResponseDto{
    public int Id { get; set; }
    public string? FileUrl { get; set; }
    public int PaymentId { get; set; }
}